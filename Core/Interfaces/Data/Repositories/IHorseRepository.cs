﻿using Core.Entities;

namespace Core.Interfaces.Data.Repositories
{
    public interface IHorseRepository
    {
        void AddArchiveHorse(HorseArchiveEntity horse);
        int AddHorse(HorseEntity horse);
        void AddRaceHorse(RaceHorseEntity horse);
        HorseEntity GetHorse(int horse_id);
        HorseArchiveEntity GetHorseArchive(int horse_id);
        HorseEntity GetHorseByRpId(int rp_id);
        void SaveChanges();
        void UpdateHorse(HorseEntity horse);
        void UpdateHorseArchive(HorseArchiveEntity horse);
        int AddJockey(JockeyEntity jockey);
        int AddTrainer(TrainerEntity trainer);
        JockeyEntity GetJockeyByName(string name);
        TrainerEntity GetTrainerByName(string name);
    }
}