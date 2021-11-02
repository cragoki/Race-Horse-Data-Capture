using Core.Interfaces.Services;
using Core.Models.Mail;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlgorithmAccuracyCalculator
{
    public class AlgorithmAccuracyCalculator : BackgroundService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IRaceService _raceService;
        private IConfigurationService _configService;
        private IMailService _mailService;
        private IAlgorithmService _algorithmService;

        public AlgorithmAccuracyCalculator(IRaceService raceService, IConfigurationService configService, IMailService mailService, IAlgorithmService algorithmService)
        {
            _raceService = raceService;
            _configService = configService;
            _mailService = mailService;
            _algorithmService = algorithmService;
        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing Algorithm Accuracy Calculator");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    Console.WriteLine("No errors, DB connection successful.");
                    Console.WriteLine($"Beginning Task at {DateTime.Now}");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("---------------------------------Accuracy Calculator Inilializing--------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");

                    var result = await _algorithmService.ExecuteActiveAlgorithm();

                    await _algorithmService.StoreAlgorithmResults(result);

                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("---------------------------------Accuracy Calculator Terminating---------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Logger.Info("-------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"completing Batch at {DateTime.Now}");

                }
                catch (Exception ex)
                {
                    var email = new MailModel()
                    {
                        ToEmail = "craigrodger1@hotmail.com",
                        Subject = "Critical Error in the RHDCBacklogAutomator",
                        Body = $"Critical Error in Accuracy Calculator, {ex.Message} shutting down Job. This will need to be repaired manually"
                    };

                    _mailService.SendEmailAsync(email);
                }
            }
        }
    }
}
