﻿using Core.Entities;
using Core.Enums;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Mail;
using Infrastructure.PunterAdmin.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCAutomation
{
    public class RHDCFetchAutomator: BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IEventService _eventService;
        private readonly IAlgorithmService _algorithmService;
        private readonly IRaceService _raceService;
        private readonly IConfigurationService _configService;
        private IMailService _mailService;
        private IMappingTableRepository _mappingRepository;
        private IFormAlgorithm _form;
        private IHorseRepository _horseRepository;

        private static Guid _batch;
        public RHDCFetchAutomator(IHostApplicationLifetime hostApplicationLifetime, ILogger<RHDCFetchAutomator> logger, IEventService eventService, IRaceService raceService, IConfigurationService configService, IMailService mailService, IMappingTableRepository mappingRepository, IFormAlgorithm form, IHorseRepository horseRepository, IAlgorithmService algorithmService)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _eventService = eventService;
            _raceService = raceService;
            _configService = configService;
            _mailService = mailService;
            _mappingRepository = mappingRepository;
            _form = form;
            _horseRepository = horseRepository;
            _algorithmService = algorithmService;
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

                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("------------------------------------Fetch Automator Inilializing---------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("-------------------------------------------------------------------------------------------------");
                        Logger.Info("                                                                                                 ");
                        Logger.Info($"                Batch Initialized with Identifier {_batch}          ");


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

                        foreach (var even in events)
                        {
                            var races = await _eventService.GetRacesFromDatabaseForAlgorithm(even.EventId);
                            var distances = _mappingRepository.GetDistanceTypes();
                            var goings = _mappingRepository.GetGoingTypes();

                            foreach (var race in races)
                            {

                                var settings = await _algorithmService.GetSettingsForAlgorithm((int)AlgorithmEnum.FormOnly);

                                var predictions = await _form.FormCalculationPredictions(race, settings, distances, goings);

                                if (predictions == null || predictions.Count() == 0)
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                {
                                    try
                                    {
                                        //store in db
                                        //Create new table for predictions
                                        var algorithmPrediction = new AlgorithmPredictionEntity() 
                                        {
                                            race_horse_id = prediction.value.RaceHorseId,
                                            algorithm_id = (int)AlgorithmEnum.FormOnly,
                                            predicted_position = prediction.i + 1,
                                            points = prediction.value.Points ?? 0
                                        };
                                        _algorithmService.AddAlgorithmPrediction(algorithmPrediction);
                                    }
                                    catch (Exception ex) 
                                    {
                                        Console.WriteLine($"Error! {ex.Message} Inner Exception: {ex.InnerException}");
                                    }
                                }
                            }
                        }

                        //Complete Diagnostics
                        diagnostics.EventsFiltered = eventsFiltered;
                        diagnostics.ErrorsEncountered = 0;
                        diagnostics.TimeCompleted = DateTime.Now;
                        diagnostics.EllapsedTime = (diagnostics.TimeCompleted - diagnostics.TimeInitialized).TotalSeconds;
                        //Store Batch in the database
                        var diagnosticsString = JsonSerializer.Serialize(diagnostics);
                        _configService.AddBatch(_batch, diagnosticsString);
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
