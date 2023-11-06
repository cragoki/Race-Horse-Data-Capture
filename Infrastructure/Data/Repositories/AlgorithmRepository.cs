using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
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

        public List<AlgorithmSettingsArchiveEntity> GetArchivedAlgorithmSettingsForBatch(Guid batch_id)
        {
            return _context.tb_algorithm_settings_archive.Where(x => x.batch_id == batch_id).ToList();
        }

        public List<AlgorithmSettingsArchiveEntity> GetArchivedAlgorithmSettings()
        {
            return _context.tb_algorithm_settings_archive.ToList();
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

        public void ArchiveAlgorithmSettings(List<AlgorithmSettingsArchiveEntity> settings)
        {
            _context.tb_algorithm_settings_archive.AddRange(settings);
            SaveChanges();
        }

        public void AddAlgorithmPrediction(AlgorithmPredictionEntity algorithmPrediction)
        {
            _context.tb_algorithm_prediction.Add(algorithmPrediction);
            SaveChanges();
        }
        public List<AlgorithmVariableSequenceEntity> GetAlgorithmVariableSequence()
        {
            return _context.tb_algorithm_variable_sequence.ToList();
        }

        public void AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence)
        {
            _context.tb_algorithm_variable_sequence.Add(sequence);
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

        public List<SequenceAnalysisEntity> GetSequenceAnalysis() 
        {
            return _context.tb_sequence_analysis.ToList();
        }

        public void AddSequenceAnalysis(SequenceAnalysisEntity entity) 
        {
            _context.tb_sequence_analysis.Add(entity);
            SaveChanges();
        }

        public void UpdateSequenceAnalysis(SequenceAnalysisEntity entity)
        {
            _context.tb_sequence_analysis.Update(entity);
            SaveChanges();
        }

        public List<SequenceCourseAccuracyEntity> GetCourseAccuracy(Guid batch)
        {
            return _context.tb_sequence_course_accuracy.Where(x => x.batch_id == batch).ToList();
        }

        public void AddCourseAccuracy(List<SequenceCourseAccuracyEntity> entities)
        {
            _context.tb_sequence_course_accuracy.AddRange(entities);
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
