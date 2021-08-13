using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System;


namespace Core.Services
{
    public class RaceService : IRaceService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;
        private readonly IEventRepository _eventRepository;

        public RaceService(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        public void GetEventRaces(int EventId)
        {
            string eventName = $"eventName - {DateTime.Now.Date}";

            try
            {
                var even = _eventRepository.GetEventById(EventId);

                Logger.Info($"Fetching Races for event {even.name}.");

                //Base URL + even.meeting_url will get you a list of all the races for that event

                Logger.Info($"Races Retrieved for event {even.name}");

            }
            catch (Exception ex)
            {
                Logger.Error($"Error attempting to retrieve todays races.  {ex.Message}");
            }
        }
    }
}
