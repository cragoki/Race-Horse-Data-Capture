using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Data.Repositories
{
    public interface IHorseRepository
    {
        Task AddArchiveHorse(HorseArchiveEntity horse);
        Task<int> AddHorse(HorseEntity horse);
        IEnumerable<RaceHorseEntity> GetRaceHorsesForRace(int raceId);
        Task AddRaceHorse(RaceHorseEntity horse);
        Task UpdateRaceHorse(RaceHorseEntity horse);
        HorseEntity GetHorse(int horse_id);
        HorseEntity GetHorseWithRaces(int horse_id);
        List<HorseArchiveEntity> GetHorseArchive(int horse_id);
        Task<HorseEntity> GetHorseByRpId(int rp_id);
        Task SaveChanges();
        Task UpdateHorse(HorseEntity horse);
        Task UpdateHorseArchive(HorseArchiveEntity horse);
        Task<int> AddJockey(JockeyEntity jockey);
        Task<int> AddTrainer(TrainerEntity trainer);
        Task<JockeyEntity> GetJockeyByName(string name);
        JockeyEntity GetJockeyById(int id);
        Task<TrainerEntity> GetTrainerByName(string name);
        TrainerEntity GetTrainerById(int id);
        IEnumerable<RaceEntity> GetNoResultRaces();
        IEnumerable<RaceHorseEntity> GetAllRacesForHorse(int horse_id);
        IEnumerable<RaceHorseEntity> GetRaceHorseWithNoPosition();
        RaceHorseEntity GetRaceHorseById(int id);
        IEnumerable<RaceHorseEntity> GetHorseRaces(int horse_id);
        IEnumerable<RaceHorseEntity> GetRaceHorseWithNoPosition(List<int> raceHorseIds);
    }
}