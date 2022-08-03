using Core.Entities;
using Core.Models.Algorithm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IAlgorithmService
    {
        Task<AlgorithmResult> ExecuteActiveAlgorithm();
        Task StoreAlgorithmResults(AlgorithmResult result);
        Task<List<AlgorithmSettingsEntity>> GetSettingsForAlgorithm(int algorithm_id);
        void AddAlgorithmPrediction(AlgorithmPredictionEntity prediction);
    }
}