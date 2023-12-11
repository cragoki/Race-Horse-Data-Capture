using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Core.Interfaces.Services
{
    public interface IRaceService
    {
        Task GetEventRaces(int EventId);
        Task<List<RaceEntity>> GetEventRacesFromDB(int EventId);
        Task GetRaceResults(RaceEntity race);
        Task<List<RaceHorseEntity>> GetIncompleteRaces();
        Task<int> GetRprForHorseRace(List<HorseArchiveEntity> archive, DateTime raceDate);
        Task<int> GetTsForHorseRace(List<HorseArchiveEntity> archive, DateTime raceDate);
        Task<int> GetMissingRaceData();
    }
}