using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Data.Repositories
{
    public interface IAlgorithmRepository
    {
        AlgorithmEntity GetActiveAlgorithm();
        AlgorithmTrackerEntity GetAlgorithmTracker(int raceHorse);
        List<AlgorithmEntity> GetAlgorithms();
        AlgorithmEntity GetAlgorithmById(int algorithmId);
        List<AlgorithmVariableEntity> GetAlgorithmVariableByAlgorithmId(int algorithmId);
        AlgorithmVariableEntity GetAlgorithmVariableById(int algorithmVariableId);
        VariableEntity GetVariableById(int variableId);
        Task SaveChanges();
        Task UpdateActiveAlgorithm(AlgorithmEntity algorithmEntity);
        Task UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> algorithmSettings);
        Task UpdateAlgorithmVariables(List<AlgorithmVariableEntity> algorithmVariables);
        Task AddAlgorithmPrediction(AlgorithmPredictionEntity algorithmPrediction);
        IQueryable<AlgorithmPredictionEntity> GetAlgorithmPrediction(int race_horse_id);
        List<AlgorithmPredictionEntity> GetAlgorithmPredictionForHorse(int horse_id);
        Task AddAlgorithmTracker(AlgorithmTrackerEntity algorithmTracker);
        Task ArchiveAlgorithmSettings(List<AlgorithmSettingsArchiveEntity> settings);
        Task AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence);
        List<AlgorithmSettingsArchiveEntity> GetArchivedAlgorithmSettings();
        List<SequenceAnalysisEntity> GetSequenceAnalysis();
        Task AddSequenceAnalysis(SequenceAnalysisEntity entity);
        Task UpdateSequenceAnalysis(SequenceAnalysisEntity entity);
        Task<List<SequenceCourseAccuracyEntity>> GetCourseAccuracy(Guid batch);
        Task AddCourseAccuracy(List<SequenceCourseAccuracyEntity> entities);
        List<AlgorithmVariableSequenceEntity> GetAlgorithmVariableSequence();
        Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedAlgorithmSettingsForBatch(Guid batch_id);
    }
}