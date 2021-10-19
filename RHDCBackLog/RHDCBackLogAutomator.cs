using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Mail;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCBackLog
{
    public class RHDCBackLogAutomator : BackgroundService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRaceService _raceService;
        private IConfigurationService _configService;
        private IMailService _mailService;
        private IEventService _eventService;
        private static Guid _batch;


        public RHDCBackLogAutomator(IRaceService raceService, IConfigurationService configService, IMailService mailService, IEventService eventService)
        {
            _raceService = raceService;
            _configService = configService;
            _mailService = mailService;
            _eventService = eventService;
        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing RHDCBackLogAutomator");

            while (!stoppingToken.IsCancellationRequested)
            { 
                try
                {
                    var job = await _configService.GetJobInfo(JobEnum.rhdcbacklog);

                    Console.WriteLine("No errors, DB connection successful.");

                    if (job.next_execution < DateTime.Now)
                    {
                        Console.WriteLine($"Beginning Batch at {DateTime.Now}");
                        _batch = Guid.NewGuid();
                        int eventsFiltered = 0;
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-----------------------------------Backlog Automator Inilializing--------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info($"                   Backlog Initialized with Identifier {_batch}                                 ");
                        //Initialize Diagnostics
                        var diagnostics = new Diagnostics()
                        {
                            Automator = AutomatorEnum.RHDCResultRetriever,
                            TimeInitialized = DateTime.Now
                        };

                        //Complete races from the database
                        await BackFill();

                        //Get next 'dates' results:
                        //await BackLog();

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
                        if (!await _configService.UpdateJob(JobEnum.rhdcbacklog))
                        {
                            //Send Error Email and stop service as the service will be broken
                            var email = new MailModel()
                            {
                                ToEmail = "craigrodger1@hotmail.com",
                                Subject = "Error in the RHDCBacklogAutomator",
                                Body = "Failed to update the job schedule, shutting down Job. This will need to be repaired manually"
                            };

                            _mailService.SendEmailAsync(email);
                        }
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
                        Subject = "Critical Error in the RHDCBacklogAutomator",
                        Body = $"Critical Error in Backlog Automator, {ex.Message} shutting down Job. This will need to be repaired manually"
                    };

                    _mailService.SendEmailAsync(email);
                }
            }
        }

        /// <summary>
        /// Backfill fills any current null race data we have in the database
        /// </summary>
        /// <returns></returns>
        public async Task BackFill() 
        {
            Logger.Info($"Retrieving existing data with missing results...");
            try
            {
                var incompleteRaces = await _raceService.GetIncompleteRaces();

                foreach (var race in incompleteRaces)
                {
                    await _raceService.GetRaceResults(race);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to process BackFill. Error: {ex.Message}, {ex.InnerException}");
            }
            Logger.Info($"Back fill complete!");
        }

        /// <summary>
        /// Backlog works its way back through results via Racing Post incrementally.
        /// </summary>
        /// <returns></returns>
        public async Task BackLog()
        {
            try
            {
                var previousDate = _configService.GetLastBackfillDate();
                var nextDate = previousDate.AddDays(-1);

                if (await _eventService.GetBacklogEvents(_batch, nextDate)) 
                {
                    await _configService.UpdateBackfillDate(nextDate);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to process BackLog. Error: {ex.Message}");
            }
        }
    }
}
