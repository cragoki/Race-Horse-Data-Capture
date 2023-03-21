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
        Task<RaceHorseStatisticsTracker> GetPastPerformance(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker);
        Task<RaceHorseStatisticsTracker> GetAdjustmentsPastPerformance(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker);
        Task<RaceHorseStatisticsTracker> GetPresentRaceFactors(RaceEntity race, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker);
        Task<RaceHorseStatisticsTracker> GetHorsePreferences(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker);
    }
}