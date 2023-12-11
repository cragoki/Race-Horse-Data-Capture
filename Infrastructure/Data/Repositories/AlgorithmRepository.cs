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

        public async Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedAlgorithmSettingsForBatch(Guid batch_id)
        {
            return await _context.tb_algorithm_settings_archive.Where(x => x.batch_id == batch_id).ToListAsync();
        }

        public List<AlgorithmSettingsArchiveEntity> GetArchivedAlgorithmSettings()
        {
            return _context.tb_algorithm_settings_archive.ToList();
        }

        public async Task UpdateActiveAlgorithm(AlgorithmEntity algorithmEntity)
        {
            _context.tb_algorithm.Update(algorithmEntity);
            await SaveChanges();
        }

        public async Task UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> algorithmSettings)
        {
            _context.tb_algorithm_settings.UpdateRange(algorithmSettings);
            await SaveChanges();
        }

        public async Task UpdateAlgorithmVariables(List<AlgorithmVariableEntity> algorithmVariables)
        {
            _context.tb_algorithm_variable.UpdateRange(algorithmVariables);
            await SaveChanges();
        }

        public async Task ArchiveAlgorithmSettings(List<AlgorithmSettingsArchiveEntity> settings)
        {
            _context.tb_algorithm_settings_archive.AddRange(settings);
            await SaveChanges();
        }

        public async Task AddAlgorithmPrediction(AlgorithmPredictionEntity algorithmPrediction)
        {
            var pendingChanges = _context.ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Unchanged)
            .ToList();

            foreach (var entry in pendingChanges)
            {
                // Check the state of the entry and the properties that are being modified
                Console.WriteLine($"Entity Name: {entry.Entity.GetType().Name}, State: {entry.State}");

                foreach (var property in entry.OriginalValues.Properties)
                {
                    var originalValue = entry.OriginalValues[property];
                    var currentValue = entry.CurrentValues[property];
                    if (originalValue != currentValue)
                    {
                        Console.WriteLine($"Property Name: {property.Name}, Original Value: {originalValue}, Current Value: {currentValue}");
                    }
                }
            }

            _context.tb_algorithm_prediction.Add(algorithmPrediction);
            await SaveChanges();
        }
        public List<AlgorithmVariableSequenceEntity> GetAlgorithmVariableSequence()
        {
            return _context.tb_algorithm_variable_sequence.ToList();
        }

        public async Task AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence)
        {
            _context.tb_algorithm_variable_sequence.Add(sequence);
            await SaveChanges();
        }

        public async Task AddAlgorithmTracker(AlgorithmTrackerEntity algorithmTracker)
        {
            _context.tb_algorithm_tracker.Add(algorithmTracker);
            await SaveChanges();
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

        public List<SequenceAnalysisEntity> GetSequenceAnalysis()
        {
            return _context.tb_sequence_analysis.ToList();
        }

        public async Task AddSequenceAnalysis(SequenceAnalysisEntity entity)
        {
            _context.tb_sequence_analysis.Add(entity);
            await SaveChanges();
        }

        public async Task UpdateSequenceAnalysis(SequenceAnalysisEntity entity)
        {
            _context.tb_sequence_analysis.Update(entity);
            await SaveChanges();
        }

        public async Task<List<SequenceCourseAccuracyEntity>> GetCourseAccuracy(Guid batch)
        {
            return await _context.tb_sequence_course_accuracy.Where(x => x.batch_id == batch).ToListAsync();
        }

        public async Task AddCourseAccuracy(List<SequenceCourseAccuracyEntity> entities)
        {
            _context.tb_sequence_course_accuracy.AddRange(entities);
            await SaveChanges();
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
