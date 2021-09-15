using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
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
        private IScraperService _scraperService;
        private IEventService _eventService;
        private static Guid _batch;

        public RHDCBackLogAutomator(IRaceService raceService, IConfigurationService configService, IScraperService scraperService, IEventService eventService)
        {
            _raceService = raceService;
            _configService = configService;
            _scraperService = scraperService;
            _eventService = eventService;
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
            //await BackFill();

            //Get next 'dates' results:
            await BackLog();

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

        /// <summary>
        /// Backfill fills any current null race data we have in the database
        /// </summary>
        /// <returns></returns>
        public async Task BackFill() 
        {
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
