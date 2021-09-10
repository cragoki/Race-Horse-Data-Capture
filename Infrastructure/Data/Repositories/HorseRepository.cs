using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System.Linq;


namespace Infrastructure.Data.Repositories
{
    public class HorseRepository : IHorseRepository
    {
        private readonly DbContextData _context;
        private readonly IConfigurationService _configService;

        public HorseEntity GetHorse(int horse_id)
        {
            return _context.tb_horse.Where(x => x.horse_id == horse_id).FirstOrDefault();
        }
        public HorseEntity GetHorseByRpId(int rp_id)
        {
            return _context.tb_horse.Where(x => x.rp_horse_id == rp_id).FirstOrDefault();
        }
        public HorseArchiveEntity GetHorseArchive(int horse_id)
        {
            return _context.tb_archive_horse.Where(x => x.horse_id == horse_id).FirstOrDefault();
        }
        public void AddHorse(HorseEntity horse)
        {
            _context.tb_horse.Add(horse);
            SaveChanges();
        }

        public void AddRaceHorse(RaceHorseEntity horse)
        {
            _context.tb_race_horse.Add(horse);
            SaveChanges();
        }

        public void AddArchiveHorse(HorseArchiveEntity horse)
        {
            _context.tb_archive_horse.Add(horse);
            SaveChanges();
        }

        public void UpdateHorse(HorseEntity horse)
        {
            _context.tb_horse.Update(horse);
            SaveChanges();
        }

        public void UpdateHorseArchive(HorseArchiveEntity horse)
        {
            _context.tb_archive_horse.Update(horse);
            SaveChanges();
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
