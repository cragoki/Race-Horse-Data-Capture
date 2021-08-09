using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class RaceService : IRaceService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;

        public RaceService(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        public void GetEventRaces(int EventId)
        {
            string eventName = $"eventName - {DateTime.Now.Date}";

            try
            {
                Logger.Info($"Fetching Races for event {eventName}.");

                Logger.Info($"Races Retrieved for event {eventName}");

            }
            catch (Exception ex)
            {
                Logger.Error($"Error attempting to retrieve todays races.  {ex.Message}");
            }
        }
    }
}
