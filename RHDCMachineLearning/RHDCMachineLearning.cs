﻿using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCMachineLearning
{
    public class RHDCMachineLearning : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IConfigurationService _configService;
        private readonly IAdjusterService _adjusterService;


        public RHDCMachineLearning(IHostApplicationLifetime hostApplicationLifetime, IConfigurationService configService, IAdjusterService adjusterService)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _configService = configService;
            _adjusterService = adjusterService;
        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool isFirstInSequence = true;
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _configService.GetJobInfo(JobEnum.rhdcalgorithmadjuster);

                if (job.start ?? false)
                {
                    Console.WriteLine($"Beginning Batch at {DateTime.Now}");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-----------------------------------Machine Learning Inilializing---------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    try
                    {
                        if (job.mode == AlgorithmModeEnum.Adjust)
                        {
                            Console.WriteLine($"Running in Adjust Mode");
                            await _adjusterService.AdjustAlgorithmSettings(isFirstInSequence);
                            isFirstInSequence = false;
                        }
                        else if (job.mode == AlgorithmModeEnum.Analyse)
                        {
                            Console.WriteLine($"Running in Analyse Mode");
                            await _adjusterService.AnalyseAlgorithmSettings();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error! {ex.Message} Inner Exception: {ex.InnerException}");
                        Thread.Sleep((int)TimeSpan.FromMinutes(10).TotalMilliseconds);
                    }
                    Console.WriteLine($"Completed Algorithm Checks");

                    //Store Batch in the database
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-----------------------------------Machine Learning Terminating----------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"completing Batch at {DateTime.Now}");

                    Thread.Sleep((int)TimeSpan.FromMinutes(5).TotalMilliseconds);
                }
                else
                {
                    Console.WriteLine($"Health check, everything Okay! The time is {DateTime.Now} Sleeping....");
                    Thread.Sleep((int)TimeSpan.FromMinutes(job.interval_check_minutes).TotalMilliseconds);
                }
            }

        }
    }
}
