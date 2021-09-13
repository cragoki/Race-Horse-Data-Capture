using Core.Entities;
using Core.Models;
using Core.Models.GetRace;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScraperService
    {
        Task<DailyRaces> RetrieveTodaysEvents();
        Task<RaceModel> RetrieveRacesForEvent(EventEntity eventEntity);
        Task<List<RaceHorseModel>> RetrieveHorseDetailsForRace(RaceEntity race);
        Task<List<RaceHorseEntity>> GetResultsForRace(RaceEntity race, List<RaceHorseEntity> raceHorses);
    }
}