using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                            .ThenInclude(x => x.Horse).FirstOrDefault();
        }
        public async Task<HorseEntity> GetHorseByRpId(int rp_id)
        {
            var result = _context.tb_horse.Include(x => x.Archive).Where(x => x.rp_horse_id == rp_id).ToList().FirstOrDefault();
            return result;
        }
        public List<HorseArchiveEntity> GetHorseArchive(int horse_id)
        {
            return _context.tb_archive_horse.Where(x => x.horse_id == horse_id).ToList();
        }
        public async Task<int> AddHorse(HorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_horse.Add(horse);
            await SaveChanges();

            return horse.horse_id;
        }
        public IEnumerable<RaceHorseEntity> GetRaceHorsesForRace(int raceId)
        {
            return _context.tb_race_horse.Where(x => x.race_id == raceId);
        }
        public async Task AddRaceHorse(RaceHorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_race_horse.Add(horse);
            await SaveChanges();
        }
        public async Task UpdateRaceHorse(RaceHorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_race_horse.Update(horse);
            await SaveChanges();
        }

        public async Task AddArchiveHorse(HorseArchiveEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_archive_horse.Add(horse);
            await SaveChanges();
        }

        public async Task UpdateHorse(HorseEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_horse.Update(horse);
            await SaveChanges();
        }

        public async Task UpdateHorseArchive(HorseArchiveEntity horse)
        {
            _context.DetachAllEntities();
            _context.tb_archive_horse.Update(horse);
            await SaveChanges();
        }

        public async Task<int> AddJockey(JockeyEntity jockey)
        {
            _context.tb_jockey.Add(jockey);
            await SaveChanges();

            return jockey.jockey_id;
        }

        public async Task<int> AddTrainer(TrainerEntity trainer)
        {
            _context.tb_trainer.Add(trainer);
            await SaveChanges();

            return trainer.trainer_id;
        }

        public async Task<JockeyEntity> GetJockeyByName(string name)
        {
            return _context.tb_jockey.Where(x => x.jockey_name == name).ToList().FirstOrDefault();
        }

        public JockeyEntity GetJockeyById(int id)
        {
            return _context.tb_jockey.Where(x => x.jockey_id == id).ToList().FirstOrDefault();
        }
        public async Task<TrainerEntity> GetTrainerByName(string name)
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
            foreach (var race in races)
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
        public IEnumerable<RaceHorseEntity> GetHorseRaces(int horse_id)
        {
            return _context.tb_race_horse.Include(x => x.Race).ThenInclude(x => x.Event).ThenInclude(x => x.MeetingType)
                .Include(x => x.Race).ThenInclude(x => x.Weather)
                .Include(x => x.Race).ThenInclude(x => x.Going)
                .Include(x => x.Race).ThenInclude(x => x.Distance)
                .Where(x => x.horse_id == horse_id).ToList();
        }

        public async Task<IEnumerable<RaceHorseEntity>> GetRaceHorseWithNoPosition()
        {
            return _context.tb_race_horse.Include(x => x.Race).ThenInclude(x => x.Event).Include(x => x.Race).ThenInclude(x => x.RaceHorses).Where(x => x.Race.Event.created >= DateTime.Now.AddMonths(-4) && x.Race.Event.created.Date < DateTime.Now.Date && x.position == 0 && String.IsNullOrEmpty(x.description)).OrderByDescending(x => x.Race.Event.created).Take(400);
        }

        public IEnumerable<RaceHorseEntity> GetRaceHorseWithNoPosition(List<int> raceHorseIds)
        {
            //SELECT top(200) e.created, CONCAT(rh.race_horse_id, ',') FROM TB_RACE_HORSE RH
            //INNER JOIN tb_race r ON rh.race_id = r.race_id
            //INNER JOIN tb_event e ON r.event_id = e.event_id
            //where rh.position = 0
            //AND DATEADD(dd, 0, DATEDIFF(dd, 0, e.created)) < DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))
            //AND(rh.Description = '' OR rh.Description IS NULL)
            //ORDER BY e.created desc
            return _context.tb_race_horse.Include(x => x.Race).ThenInclude(x => x.Event).Include(x => x.Race).ThenInclude(x => x.RaceHorses).Where(x => raceHorseIds.Contains(x.race_horse_id));
        }

        public RaceHorseEntity GetRaceHorseById(int id)
        {
            return _context.tb_race_horse.Include(x => x.Race).Include(x => x.Horse).Where(x => x.race_horse_id == id).FirstOrDefault();
        }

        public async Task SaveChanges()
        {
            if (_configService.SavePermitted())
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
