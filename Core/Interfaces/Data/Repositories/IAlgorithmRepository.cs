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
        void SaveChanges();
        void UpdateActiveAlgorithm(AlgorithmEntity algorithmEntity);
        void UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> algorithmSettings);
        void UpdateAlgorithmVariables(List<AlgorithmVariableEntity> algorithmVariables);
        void AddAlgorithmPrediction(AlgorithmPredictionEntity algorithmPrediction);
        IQueryable<AlgorithmPredictionEntity> GetAlgorithmPrediction(int race_horse_id);
        List<AlgorithmPredictionEntity> GetAlgorithmPredictionForHorse(int horse_id);
        void AddAlgorithmTracker(AlgorithmTrackerEntity algorithmTracker);
        void ArchiveAlgorithmSettings(List<AlgorithmSettingsArchiveEntity> settings);
        void AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence);
        List<AlgorithmSettingsArchiveEntity> GetArchivedAlgorithmSettings();
        List<SequenceAnalysisEntity> GetSequenceAnalysis();
        void AddSequenceAnalysis(SequenceAnalysisEntity entity);
        void UpdateSequenceAnalysis(SequenceAnalysisEntity entity);
        List<SequenceCourseAccuracyEntity> GetCourseAccuracy(Guid batch);
        void AddCourseAccuracy(List<SequenceCourseAccuracyEntity> entities);
        List<AlgorithmVariableSequenceEntity> GetAlgorithmVariableSequence();
        List<AlgorithmSettingsArchiveEntity> GetArchivedAlgorithmSettingsForBatch(Guid batch_id);
    }
}