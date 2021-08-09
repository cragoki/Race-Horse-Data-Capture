namespace Core.Interfaces.Services
{
    public interface IScraperService
    {
        void RetrieveTodaysEvents();

        void RetrieveRacesForEvent(int eventId);
    }
}