using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IHorseRepository
    {
        void AddArchiveHorse(HorseArchiveEntity horse);
        int AddHorse(HorseEntity horse);
        IEnumerable<RaceHorseEntity> GetRaceHorsesForRace(int raceId);
        void AddRaceHorse(RaceHorseEntity horse);
        void UpdateRaceHorse(RaceHorseEntity horse);
        HorseEntity GetHorse(int horse_id);
        List<HorseArchiveEntity> GetHorseArchive(int horse_id);
        HorseEntity GetHorseByRpId(int rp_id);
        void SaveChanges();
        void UpdateHorse(HorseEntity horse);
        void UpdateHorseArchive(HorseArchiveEntity horse);
        int AddJockey(JockeyEntity jockey);
        int AddTrainer(TrainerEntity trainer);
        JockeyEntity GetJockeyByName(string name);
        JockeyEntity GetJockeyById(int id);
        TrainerEntity GetTrainerByName(string name);
        TrainerEntity GetTrainerById(int id);
        IEnumerable<RaceEntity> GetNoResultRaces();
        IEnumerable<RaceHorseEntity> GetAllRacesForHorse(int horse_id);
    }
}