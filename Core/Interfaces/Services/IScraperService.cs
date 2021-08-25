using Core.Entities;
using Core.Models;
using Core.Models.GetRace;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScraperService
    {
        Task<DailyRaces> RetrieveTodaysEvents();

        Task<RaceModel> RetrieveRacesForEvent(EventEntity eventEntity);

    }
}