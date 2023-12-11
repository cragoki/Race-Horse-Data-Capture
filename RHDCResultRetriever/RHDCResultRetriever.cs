using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Mail;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCResultRetriever
{
    public class RHDCResultRetriever : BackgroundService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static Guid _batch;
        private static IEventService _eventService;
        private static IRaceService _raceService;
        private readonly IConfigurationService _configService;
        private IMailService _mailService;

        public RHDCResultRetriever(IEventService eventService, IRaceService raceService, IConfigurationService configService, IMailService mailService)
        {
            _eventService = eventService;
            _raceService = raceService;
            _configService = configService;
            _mailService = mailService;
        }

        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing RHDCResultRetriever");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var job = await _configService.GetJobInfo(JobEnum.rhdcresultretriever);

                    Console.WriteLine("No errors, DB connection successful.");

                    if (job.next_execution < DateTime.Now)
                    {
                        Console.WriteLine($"Beginning Batch at {DateTime.Now}");
                        _batch = Guid.NewGuid();
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-----------------------------------Result Automator Inilializing---------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("                                                                                                 ");
                        Logger.Info($"                  Batch Initialized with Identifier {_batch}                                    ");

                        //Initialize Diagnostics
                        var diagnostics = new Diagnostics()
                        {
                            Automator = AutomatorEnum.RHDCResultRetriever,
                            TimeInitialized = DateTime.Now
                        };

                        //Get todays events from the database based on date
                        var events = await _eventService.GetEventsFromDatabase();
                        Logger.Info($"Retrieved {events.Count()} events from the database");

                        // foreach event, get all races
                        foreach (var even in events)
                        {
                            var races = await _raceService.GetEventRacesFromDB(even.event_id);
                            //races.AddRange(await _raceService.GetIncompleteRaces());
                            Logger.Info($"Retrieved {races.Count()} races for event {even.name}");

                            //foreach race convert the race url into a result URL and scrape the results into tb_race_horse
                            foreach (var race in races)
                            {
                                Logger.Info($"Getting race results for {race.race_id}");
                                try
                                {
                                    if (race.RaceHorses != null && race.RaceHorses.Count() > 0)
                                    {
                                        await _raceService.GetRaceResults(race);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.Info($"Error Getting race results for Race {race.race_id}");
                                }
                            }
                        }

                        var diagnosticsString = JsonSerializer.Serialize(diagnostics);
                        Logger.Info(diagnosticsString);
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-----------------------------------Automator Terminating-----------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"completing Batch at {DateTime.Now}");

                        //Update Job Info
                        await _configService.UpdateJob(JobEnum.rhdcresultretriever);
                    }
                    else
                    {
                        Console.WriteLine($"Health check, everything Okay! The time is {DateTime.Now} Sleeping....");
                    }

                    //Get the Interval_Minutes from the DB to set the interval time
                    Thread.Sleep((int)TimeSpan.FromMinutes(job.interval_check_minutes).TotalMilliseconds);
                }
                catch (Exception ex)
                {
                    var email = new MailModel()
                    {
                        ToEmail = "craigrodger1@hotmail.com",
                        Subject = "Critical Error in the RHDCResultRetriever",
                        Body = $"Critical Error in Result Retriever Automator, {ex.Message} shutting down Job. This will need to be repaired manually"
                    };

                    _mailService.SendEmailAsync(email);
                }
            }

        }

    }
}
