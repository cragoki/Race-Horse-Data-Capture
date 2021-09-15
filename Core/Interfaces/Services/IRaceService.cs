using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IRaceService
    {
        Task GetEventRaces(int EventId);
        Task<List<RaceEntity>> GetEventRacesFromDB(int EventId);
        Task GetRaceResults(RaceEntity race);
        Task<List<RaceEntity>> GetIncompleteRaces();
    }
}