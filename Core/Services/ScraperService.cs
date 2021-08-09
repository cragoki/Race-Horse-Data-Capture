using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class ScraperService : IScraperService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ScraperService()
        {

        }

        public void RetrieveTodaysEvents() 
        {

            //Retrieve the events for today
            try
            {
                //We will need to fetch and return an event object here...

            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Failed to retrieve todays events.  Terminating process. {ex.Message} !!!");
                throw new Exception(ex.Message);
            }
        }

        public void RetrieveRacesForEvent(int eventId)
        {
            //Retrieve the event info from the DB
            try
            {

                //We will need to fetch and return a race object here...

            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve races for event {eventId}");
            }
        }
    }
}
