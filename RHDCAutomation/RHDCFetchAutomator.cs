using Core.Entities;
using Core.Enums;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Mail;
using Infrastructure.PunterAdmin.ViewModels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCAutomation
{
    public class RHDCFetchAutomator : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IEventService _eventService;
        private readonly IAlgorithmService _algorithmService;
        private readonly IRaceService _raceService;
        private readonly IConfigurationService _configService;
        private IMailService _mailService;
        private IBentnersModel _bentnersModel;
        private IMyModel _myModel;

        private static Guid _batch;
        public RHDCFetchAutomator(IHostApplicationLifetime hostApplicationLifetime, ILogger<RHDCFetchAutomator> logger, IEventService eventService, IRaceService raceService, IConfigurationService configService, IMailService mailService, IAlgorithmService algorithmService, IBentnersModel bentnersModel, IMyModel myModel)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _eventService = eventService;
            _raceService = raceService;
            _configService = configService;
            _mailService = mailService;
            _algorithmService = algorithmService;
            _bentnersModel = bentnersModel;
            _myModel = myModel;
        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing RHDCFetchAutomator");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Checking DB...");

                    var job = await _configService.GetJobInfo(JobEnum.rhdcautomation);
                    Console.WriteLine("No errors, DB connection successful.");

                    var adjuster = await _configService.GetJobInfo(JobEnum.rhdcalgorithmadjuster);
                    bool wait = false;

                    if (job.next_execution < DateTime.Now && adjuster.start == true) 
                    {
                        await _configService.UpdateJob(JobEnum.rhdcalgorithmadjuster);
                    }



                    if (job.next_execution < DateTime.Now && !wait)
                    {
                        Console.WriteLine($"Beginning Batch at {DateTime.Now}");
                        _batch = Guid.NewGuid();
                        int eventsFiltered = 0;

                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("------------------------------------Fetch Automator Inilializing---------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("                                                                                                 ");
                        Console.WriteLine($"                Batch Initialized with Identifier {_batch}          ");


                        //Initialize Diagnostics
                        var diagnostics = new Diagnostics()
                        {
                            Automator = AutomatorEnum.RHDCFetch,
                            TimeInitialized = DateTime.Now
                        };

                        //Get todays Events
                        var events = await _eventService.GetTodaysEvents(_batch);

                        foreach (var even in events)
                        {
                            //Retrieve and store races
                            await _raceService.GetEventRaces(even.EventId);

                            eventsFiltered++;
                        }

                        //Trigger Algorithm for todays results.
                        Console.WriteLine($"Running Predictions");

                        var activeAlgorithm = _algorithmService.GetActiveAlgorithm();

                        try
                        {
                            if (activeAlgorithm == null)
                            {
                                Console.WriteLine($"No Active Algorithm Detected");
                            }
                            else
                            {
                                Console.WriteLine($"Running Active Algorithm {activeAlgorithm.algorithm_name}...");
                                var predictions = new List<List<FormResultModel>>();

                                foreach (var even in events)
                                {
                                    var races = await _eventService.GetRacesFromDatabaseForAlgorithm(even.EventId);

                                    foreach (var race in races)
                                    {
                                        var prediction = new List<FormResultModel>();
                                        //if (activeAlgorithm.algorithm_id == (int)AlgorithmEnum.FormOnly)
                                        //{
                                        //    predictions = await _form.FormCalculationPredictions(race, settings, distances, goings);
                                        //}
                                        //else if (activeAlgorithm.algorithm_id == (int)AlgorithmEnum.FormRevamp)
                                        //{
                                        //    predictions = await _formRevampedAlgorithm.FormCalculationPredictions(race, settings, distances, goings);
                                        //}
                                        if (activeAlgorithm.algorithm_id == (int)AlgorithmEnum.BentnersModel)
                                        {
                                            prediction = await _bentnersModel.RunModel(race);
                                        }
                                        else if (activeAlgorithm.algorithm_id == (int)AlgorithmEnum.MyModel)
                                        {
                                            prediction = await _myModel.RunModel(race);

                                        }
                                        if (prediction == null || prediction.Count() == 0)
                                        {
                                            continue;
                                        }
                                        predictions.Add(prediction);
                                    }
                                }

                                for (int i = 0; i < predictions.Count(); i++)
                                {
                                    foreach (var prediction in predictions[i].OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                    {
                                        try
                                        {
                                            //store in db
                                            //Create new table for predictions
                                            var algorithmPrediction = new AlgorithmPredictionEntity()
                                            {
                                                race_horse_id = prediction.value.RaceHorseId,
                                                algorithm_id = activeAlgorithm.algorithm_id,
                                                predicted_position = prediction.i + 1,
                                                points = prediction.value.Points ?? 0,
                                                points_description = prediction.value.PointsDescription,
                                                horse_predictability = prediction.value.Predictability
                                            };
                                            _algorithmService.AddAlgorithmPrediction(algorithmPrediction);
                                            if (prediction.value.Tracker != null)
                                            {
                                                _algorithmService.AddAlgorithmTracker(prediction.value.Tracker);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Error! {ex.Message} Inner Exception: {ex.InnerException}");
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error Occured while running predictions. All Races have still been saved. Error: {ex.Message} ---- Inner Exception: {ex.InnerException}");
                        }


                        Console.WriteLine($"Completed Algorithm Checks");

                        //Complete Diagnostics
                        diagnostics.EventsFiltered = eventsFiltered;
                        diagnostics.ErrorsEncountered = 0;
                        diagnostics.TimeCompleted = DateTime.Now;
                        diagnostics.EllapsedTime = (diagnostics.TimeCompleted - diagnostics.TimeInitialized).TotalSeconds;
                        //Store Batch in the database
                        var diagnosticsString = JsonSerializer.Serialize(diagnostics);
                        _configService.AddBatch(_batch, diagnosticsString);
                        Console.WriteLine(diagnosticsString);
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-----------------------------------Automator Terminating-----------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"completing Batch at {DateTime.Now}");

                        //Update Job Info
                        if (!await _configService.UpdateJob(JobEnum.rhdcautomation))
                        {
                            //Send Error Email and stop service as the service will be broken
                            var email = new MailModel()
                            {
                                ToEmail = "craigrodger1@hotmail.com",
                                Subject = "Error in the RHDCBacklogAutomator",
                                Body = "Failed to update the job schedule, shutting down Job. This will need to be repaired manually"
                            };

                            await _mailService.SendEmailAsync(email);
                        }

                        //TEMPORARY - To test the service is executing as expected
                        var success = new MailModel()
                        {
                            ToEmail = "craigrodger1@hotmail.com",
                            Subject = "RHDCBacklogAutomator EXECUTED!",
                            Body = "Everything Okay."
                        };

                        await _mailService.SendEmailAsync(success);
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
                    Console.WriteLine($"Error! {ex.Message} Inner Exception: {ex.InnerException}");
                    Thread.Sleep((int)TimeSpan.FromMinutes(10).TotalMilliseconds);

                    var email = new MailModel()
                    {
                        ToEmail = "craigrodger1@hotmail.com",
                        Subject = "Critical Error in the RHDCFetchAutomater",
                        Body = $"Critical Error in Fetch Automator, {ex.Message} shutting down Job. This will need to be repaired manually"
                    };

                    await _mailService.SendEmailAsync(email);
                }
            }
        }
    }
}
