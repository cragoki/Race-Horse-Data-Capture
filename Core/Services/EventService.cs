using Core.Interfaces.Services;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;

        public EventService(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        public List<Event> GetTodaysEvents()
        {
            var result = new List<Event>();
            int totalEvents = 0;

            try
            {
                Logger.Info("Fetching Todays Events.");

                _scraperService.RetrieveTodaysEvents();

                Logger.Info($"{totalEvents} Events Retrieved and stored.");
            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Error attempting to retrieve todays races. Terminating process. {ex.Message} !!!");
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
