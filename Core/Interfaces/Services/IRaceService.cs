using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
namespace Core.Interfaces.Services
{
    public interface IRaceService
    {
        Task GetEventRaces(int EventId);
        Task<List<RaceEntity>> GetEventRacesFromDB(int EventId);
        Task GetRaceResults(RaceEntity race);
        Task<List<RaceEntity>> GetIncompleteRaces();
        Task<int> GetRprForHorseRace(List<HorseArchiveEntity> archive, DateTime raceDate);
        Task<int> GetTsForHorseRace(List<HorseArchiveEntity> archive, DateTime raceDate);
    }
}