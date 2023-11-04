using Core.Entities;
using Core.Models.Algorithm;
using Core.Models.MachineLearning;
using Infrastructure.PunterAdmin.ViewModels;
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
        void AddAlgorithmPrediction(AlgorithmPredictionEntity prediction);
        void AddAlgorithmTracker(AlgorithmTrackerEntity tracker);
        VariableAnalysisModel IdentifyWinningAlgorithmVariables(List<AlgorithmPredictionEntity> predictions, List<AlgorithmTrackerEntity> trackers, RaceEntity race);
        void ArchiveAlgorithmSettings(List<AlgorithmSettingsEntity> settings, Guid batchId);
        Task UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> settings);
        void AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence);
        Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedSettingsForAlgorithm();
        Task<bool> AlgorithmSettingsAreUnique(List<AlgorithmSettingsEntity> settings);
    }
}