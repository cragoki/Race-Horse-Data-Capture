using Core.Entities;
using Core.Models.Algorithm;
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
    }
}