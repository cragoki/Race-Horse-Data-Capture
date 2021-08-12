using Core.Interfaces.Services;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Event>> GetTodaysEvents()
        {
            var result = new List<Event>();

            try
            {
                Logger.Info("Fetching Todays Events.");

                var todaysRaces = await _scraperService.RetrieveTodaysEvents();

                //Store todays race information with batch

                //Return necessary information
                foreach (var even in todaysRaces.Courses)
                {
                    var res = new Event()
                    {
                        EventId = even.Id
                    };

                    result.Add(res);
                }

                Logger.Info($"{todaysRaces.Courses.Count} Events Retrieved and stored successfully.");
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
