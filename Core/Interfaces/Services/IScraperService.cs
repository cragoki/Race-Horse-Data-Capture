using Core.Models.GetRace;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScraperService
    {
        Task<DailyRaces> RetrieveTodaysEvents();

        void RetrieveRacesForEvent(int eventId);
    }
}