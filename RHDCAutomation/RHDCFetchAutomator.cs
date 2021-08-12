using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
using Infrastructure.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
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
        public RHDCFetchAutomator(IHostApplicationLifetime hostApplicationLifetime, ILogger<RHDCFetchAutomator> logger, IEventService eventService, IRaceService raceService)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _eventService = eventService;
            _raceService = raceService;

        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int racesFiltered = 0;
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("------------------------------------Fetch Automator Inilializing---------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            //Initialize Diagnostics
            var diagnostics = new Diagnostics()
            {
                Automator = AutomatorEnum.RHDCFetch,
                TimeInitialized = DateTime.Now
            };

            //Get todays Events
            var events = await _eventService.GetTodaysEvents();

            foreach (var even in events) 
            {
                //Retrieve and store races
                _raceService.GetEventRaces(even.EventId);

                racesFiltered++;
            }

            //Complete Diagnostics
            diagnostics.EventsFiltered = racesFiltered;
            diagnostics.ErrorsEncountered = 0;
            diagnostics.TimeCompleted = DateTime.Now;
            diagnostics.EllapsedTime = (diagnostics.TimeCompleted - diagnostics.TimeInitialized).TotalSeconds;
            Logger.Info(JsonSerializer.Serialize(diagnostics));
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
