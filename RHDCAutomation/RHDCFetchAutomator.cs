﻿using Core.Entities;
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

                    if (job.next_execution < DateTime.Now)
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
                        Console.WriteLine("Retrieving Todays Events");
                        var events = await _eventService.GetTodaysEvents(_batch);
                        //var events = await _eventService.GetEventsFromDatabase();
                        Console.WriteLine("Retrieved Todays Events");
                        foreach (var even in events)
                        {
                            Console.WriteLine($"Getting Event Race for event id: {even.EventId}");
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

                                var settings = await _algorithmService.GetArchivedSettingsForBatch(activeAlgorithm.active_batch.Value);
                                //Reset Algorithm settings
                                //await _algorithmService.AdjustAlgorithmSettings(activeAlgorithm.active_batch.Value, activeAlgorithm); < - This is preventing tb_job from updating

                                var eventEntities = await _eventService.GetEventsFromDatabase();
                                foreach (var even in eventEntities)
                                {
                                    var races = await _eventService.GetRacesFromDatabaseForAlgorithm(even.event_id);

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
                                            if (prediction.value.Tracker != null && prediction.value.Tracker.race_horse_id != 0)
                                            {
                                                await _algorithmService.AddAlgorithmTracker(prediction.value.Tracker);

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
                                                await _algorithmService.AddAlgorithmPrediction(algorithmPrediction);
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
                        await _configService.AddBatch(_batch, diagnosticsString);
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
                        Console.WriteLine($"Updating Job Next Execution Time");
                        var tomorrow = DateTime.Now.AddDays(1);
                        job.last_execution = DateTime.Now;
                        job.next_execution = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 06, 00, 00);
                        
                        await _configService.UpdateJob(job);


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
                }
            }
        }
    }
}
