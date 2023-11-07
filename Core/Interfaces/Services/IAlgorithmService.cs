using Core.Entities;
using Core.Models.Algorithm;
using Core.Models.MachineLearning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IAlgorithmService
    {
        Task<AlgorithmResult> ExecuteActiveAlgorithm();
        AlgorithmEntity GetActiveAlgorithm();
        Task StoreAlgorithmResults(AlgorithmResult result);
        Task<List<AlgorithmSettingsEntity>> GetSettingsForAlgorithm(int algorithm_id);
        Task AddAlgorithmPrediction(AlgorithmPredictionEntity prediction);
        Task AddAlgorithmTracker(AlgorithmTrackerEntity tracker);
        VariableAnalysisModel IdentifyWinningAlgorithmVariables(List<AlgorithmPredictionEntity> predictions, List<AlgorithmTrackerEntity> trackers, RaceEntity race);
        Task ArchiveAlgorithmSettings(List<AlgorithmSettingsEntity> settings, Guid batchId);
        Task UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> settings);
        Task AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence);
        Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedSettingsForAlgorithm();
        Task<bool> AlgorithmSettingsAreUnique(List<AlgorithmSettingsEntity> settings);
        Task<List<SequenceAnalysisEntity>> GetSequenceAnalysis();
        Task<List<AlgorithmVariableSequenceEntity>> GetAlgorithmVariableSequence();
        Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedSettingsForBatch(Guid batch_id);
        Task<List<SequenceCourseAccuracyEntity>> GetSequenceCourseAccuracy(Guid batch_id);
        Task AddSequenceAnalysis(SequenceAnalysisEntity entity);
        Task UpdateSequenceAnalysis(SequenceAnalysisEntity entity);
        Task AddCourseAccuracy(List<SequenceCourseAccuracyEntity> entites);
    }
}