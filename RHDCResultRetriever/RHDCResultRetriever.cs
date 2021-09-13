using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCResultRetriever
{
    public class RHDCResultRetriever: BackgroundService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static Guid _batch;
        private static IEventService _eventService;
        private static IRaceService _raceService;

        public RHDCResultRetriever(IEventService eventService, IRaceService raceService)
        {
            _eventService = eventService;
            _raceService = raceService;
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
            Logger.Info("-----------------------------------Result Automator Inilializing---------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("-------------------------------------------------------------------------------------------------");
            Logger.Info("                                                                                                 ");
            Logger.Info($"                Batch Initialized with Identifier {_batch}          ");

            //Initialize Diagnostics
            var diagnostics = new Diagnostics()
            {
                Automator = AutomatorEnum.RHDCResultRetriever,
                TimeInitialized = DateTime.Now
            };

            //Get todays events from the database based on date
            var events = await _eventService.GetEventsFromDatabase();

            // foreach event, get all races
            foreach (var even in events) 
            {
                var races = await _raceService.GetEventRacesFromDB(even.event_id);

                //foreach race convert the race url into a result URL and scrape the results into tb_race_horse
                foreach (var race in races) 
                {
                    await _raceService.GetRaceResults(race);
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

        }

    }
}
