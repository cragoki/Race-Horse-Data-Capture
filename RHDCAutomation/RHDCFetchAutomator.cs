﻿using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Mail;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IRaceService _raceService;
        private readonly IConfigurationService _configService;
        private IMailService _mailService;

        private static Guid _batch;
        public RHDCFetchAutomator(IHostApplicationLifetime hostApplicationLifetime, ILogger<RHDCFetchAutomator> logger, IEventService eventService, IRaceService raceService, IConfigurationService configService, IMailService mailService)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
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
