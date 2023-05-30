using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class AlgorithmRepository : IAlgorithmRepository
    {
        private readonly DbContextData _context;
        private readonly IConfigurationService _configService;

        public AlgorithmRepository(DbContextData context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService;
        }

        public List<AlgorithmEntity> GetAlgorithms()
        {
            return _context.tb_algorithm.AsNoTracking().Include(x => x.Settings).Include(x => x.Variables).ToList();

        }

        public AlgorithmEntity GetAlgorithmById(int algorithmId)
        {
            return _context.tb_algorithm.Include(x => x.Variables).Include(x => x.Settings).Where(x => x.algorithm_id == algorithmId).ToList().FirstOrDefault();

        }

        public VariableEntity GetVariableById(int variableId)
        {
            return _context.tb_variable.Where(x => x.variable_id == variableId).ToList().FirstOrDefault();

        }

        public AlgorithmVariableEntity GetAlgorithmVariableById(int algorithmVariableId)
        {
            return _context.tb_algorithm_variable.Where(x => x.algorithm_variable_id == algorithmVariableId).ToList().FirstOrDefault();
        }
        public List<AlgorithmVariableEntity> GetAlgorithmVariableByAlgorithmId(int algorithmId)
        {
            return _context.tb_algorithm_variable.Where(x => x.algorithm_id == algorithmId).ToList();
        }

        public AlgorithmEntity GetActiveAlgorithm()
        {
            return _context.tb_algorithm.Where(x => x.active == true).ToList().FirstOrDefault();
        }

        public AlgorithmTrackerEntity GetAlgorithmTracker(int raceHorse)
        {
            return _context.tb_algorithm_tracker.Where(x => x.race_horse_id == raceHorse).OrderByDescending(x => x.created).FirstOrDefault();
        }

        public void UpdateActiveAlgorithm(AlgorithmEntity algorithmEntity)
        {
            _context.tb_algorithm.Update(algorithmEntity);
            SaveChanges();
        }

        public void UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> algorithmSettings)
        {
            _context.tb_algorithm_settings.UpdateRange(algorithmSettings);
            SaveChanges();
        }

        public void UpdateAlgorithmVariables(List<AlgorithmVariableEntity> algorithmVariables)
        {
            _context.tb_algorithm_variable.UpdateRange(algorithmVariables);
            SaveChanges();
        }

        public void AddAlgorithmPrediction(AlgorithmPredictionEntity algorithmPrediction)
        {
            _context.tb_algorithm_prediction.Add(algorithmPrediction);
            SaveChanges();
        }

        public void AddAlgorithmTracker(AlgorithmTrackerEntity algorithmTracker)
        {
            _context.tb_algorithm_tracker.Add(algorithmTracker);
            SaveChanges();
        }

        public IQueryable<AlgorithmPredictionEntity> GetAlgorithmPrediction(int race_horse_id)
        {
            return _context.tb_algorithm_prediction.Where(x => x.race_horse_id == race_horse_id).AsQueryable();
        }
        public List<AlgorithmPredictionEntity> GetAlgorithmPredictionForHorse(int horse_id)
        {
            return _context.tb_algorithm_prediction
                .Include(x => x.RaceHorse)
                    .ThenInclude(x => x.Horse)
                .Include(x => x.RaceHorse)
                    .ThenInclude(x => x.Race)
                        .ThenInclude(x => x.Event)
                .Where(x => x.RaceHorse.Horse.horse_id == horse_id).ToList();
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
