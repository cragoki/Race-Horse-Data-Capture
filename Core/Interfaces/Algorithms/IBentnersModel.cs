using Core.Entities;
using Core.Models.Algorithm;
using Core.Models.Algorithm.Bentners;
using Infrastructure.PunterAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Algorithms
{
    public interface IBentnersModel
    {
        Task<List<FormResultModel>> RunModel(RaceEntity race);
        Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races);
        Task<List<HorsePredictionModel>> GetHorsePoints(List<RaceHorseEntity> horses, RaceEntity race, List<AlgorithmSettingsEntity> settings);
        Task<RaceHorseStatisticsTracker> GetCurrentCondition(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker);
        Task<decimal> GetPastPerformance(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings);
        Task<decimal> GetAdjustmentsPastPerformance(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings);
        Task<decimal> GetPresentRaceFactors(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings);
        Task<decimal> GetHorsePreferences(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings);
    }
}