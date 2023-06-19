using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Infrastructure.Data.Repositories
{
    public class HorseRepository : IHorseRepository
    {
        private readonly DbContextData _context;
        private readonly IConfigurationService _configService;

        public HorseRepository(DbContextData context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService;
        }

        public HorseEntity GetHorse(int horse_id)
        {
            return _context.tb_horse.Where(x => x.horse_id == horse_id).ToList().FirstOrDefault();
        }
        public HorseEntity GetHorseWithRaces(int horse_id)
        {
            return _context.tb_horse.Where(x => x.horse_id == horse_id)
                .Include(x => x.Archive)
                .Include(x => x.Races)
                        .ThenInclude(z => z.Jockey)
                .Include(x => x.Races)
                        .ThenInclude(z => z.Race)
                            .ThenInclude(y => y.Event)
                .Include(x => x.Races)
                        .ThenInclude(z => z.Trainer)
                .Include(x => x.Races)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(y => y.Weather)
                .Include(x => x.Races)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(y => y.Stalls)
                .Include(x => x.Races)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(y => y.Distance)
                .Include(x => x.Races)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(y => y.Ages)
                .Include(x => x.Races)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(y => y.Going)
                .Include(x => x.Races)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(x => x.RaceHorses)
                            .ThenInclude(x => x.Horse)
                .ToList().FirstOrDefault();
        }
        public HorseEntity GetHorseByRpId(int rp_id)
        {
            var result = _context.tb_horse.Include(x => x.Archive).Where(x => x.rp_horse_id == rp_id).ToList().FirstOrDefault();
            return result;
        }
        public List<HorseArchiveEntity> GetHorseArchive(int horse_id)
        {
            return _context.tb_archive_horse.Where(x => x.horse_id == horse_id).ToList();
        }
        public int AddHorse(HorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_horse.Add(horse);
            SaveChanges();

            return horse.horse_id;
        }
        public IEnumerable<RaceHorseEntity> GetRaceHorsesForRace(int raceId)
        {
            return _context.tb_race_horse.Where(x => x.race_id == raceId);
        }
        public void AddRaceHorse(RaceHorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_race_horse.Add(horse);
            SaveChanges();
        }
        public void UpdateRaceHorse(RaceHorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_race_horse.Update(horse);
            SaveChanges();
        }

        public void AddArchiveHorse(HorseArchiveEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_archive_horse.Add(horse);
            SaveChanges();
        }

        public void UpdateHorse(HorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_horse.Update(horse);
            SaveChanges();
        }

        public void UpdateHorseArchive(HorseArchiveEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_archive_horse.Update(horse);
            SaveChanges();
        }

        public int AddJockey(JockeyEntity jockey)
        {
            _context.DetachAllEntities();
            _context.tb_jockey.Add(jockey);
            SaveChanges();

            return jockey.jockey_id;
        }

        public int AddTrainer(TrainerEntity trainer)
        {
            _context.DetachAllEntities();
            _context.tb_trainer.Add(trainer);
            SaveChanges();

            return trainer.trainer_id;
        }

        public JockeyEntity GetJockeyByName(string name)
        {
            return _context.tb_jockey.Where(x => x.jockey_name == name).ToList().FirstOrDefault();
        }

        public JockeyEntity GetJockeyById(int id) 
        {
            return _context.tb_jockey.Where(x => x.jockey_id == id).ToList().FirstOrDefault();
        }
        public TrainerEntity GetTrainerByName(string name)
        {
            return _context.tb_trainer.Where(x => x.trainer_name == name).FirstOrDefault();
        }
        public TrainerEntity GetTrainerById(int id)
        {
            return _context.tb_trainer.Where(x => x.trainer_id == id).ToList().FirstOrDefault();
        }

        public IEnumerable<RaceEntity> GetNoResultRaces()
        {
           var result = new List<RaceEntity>();
           var races = _context.tb_race_horse.Where(x => x.position == 0).Select(x => x.Race);
           foreach(var race in races) 
           {
                if (race.RaceHorses.All(x => x.position == 0) && race.Event.created.Date < DateTime.Now.Date) 
                {
                    result.Add(race);
                }
           }

            return result;
        }
        public IEnumerable<RaceHorseEntity> GetAllRacesForHorse(int horse_id)
        {
            return _context.tb_race_horse.Where(x => x.horse_id == horse_id && x.position > 0).ToList();
        }

        public IEnumerable<RaceHorseEntity> GetRaceHorseWithNoPosition()
        {
            return _context.tb_race_horse.Include(x => x.Race).ThenInclude(x => x.Event).Include(x => x.Race).ThenInclude(x => x.RaceHorses).Where(x => x.Race.Event.created >= DateTime.Now.AddMonths(-4) && x.position == 0 && x.description != "NR" && x.description != "ERROR" && !x.Race.RaceHorses.All(x => x.position == 0)).Take(1000);
        }

        public RaceHorseEntity GetRaceHorseById(int id)
        {
            return _context.tb_race_horse.Include(x => x.Race).Include(x => x.Horse).Where(x => x.race_horse_id == id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            if (_configService.SavePermitted())
            {
                _context.SaveChanges();
            }
        }
    }
}
