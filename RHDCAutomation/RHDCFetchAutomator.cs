using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
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
        private static Guid _batch;
        public RHDCFetchAutomator(IHostApplicationLifetime hostApplicationLifetime, ILogger<RHDCFetchAutomator> logger, IEventService eventService, IRaceService raceService, IConfigurationService configService)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _eventService = eventService;
            _raceService = raceService;
            _configService = configService;
        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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
        }
    }
}
