using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Core.Algorithms
{
    public class BentnersModel : IBentnersModel
    {
        private readonly IConfigurationRepository _configRepository;
        private readonly IMappingTableRepository _mappingRepository;
        private readonly IAlgorithmRepository _algorithmRepository;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public BentnersModel(IConfigurationRepository configRepository, IMappingTableRepository mappingRepository, IAlgorithmRepository algorithmRepository)
        {
            _configRepository = configRepository;
            _mappingRepository = mappingRepository;
            _algorithmRepository = algorithmRepository;
        }

        public async Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races, List<AlgorithmVariableEntity> algorithms)
        {
            var result = new AlgorithmResult();
            try
            {
                List<double> runningTotal = new List<double>();
                int raceCounter = 0;

                foreach (var race in races)
                {
                    var predictionResult = await FormCalculation(race, algorithms);

                    if (predictionResult != -1)
                    {
                        Console.WriteLine($"Valid Prediction with {predictionResult}%");
                        raceCounter++;
                        runningTotal.Add(predictionResult);
                    }
                }

                //get average of running total and store result
                if (raceCounter > 0)
                {
                    result.Accuracy = (decimal)runningTotal.Average();
                    result.RacesFiltered = raceCounter;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task<double> FormCalculation(RaceEntity race, List<AlgorithmVariableEntity> variables)
        {
            return 0;
        }

        public async Task<List<FormResultModel>> FormCalculationPredictions(RaceEntity race, List<AlgorithmSettingsEntity> settings, List<DistanceType> distances, List<GoingType> goings)
        {
            var result = new List<FormResultModel>();

            return result;
        }

        private async Task<string> CalculateHorsePredictability(int horseId, DateTime dateOfRace, RaceEntity race, GoingGroupModel goingGroup, DistanceGroupModel distanceGroup)
        {
            var result = 0M;

            return result.ToString("0.00");
        }

        private async Task<List<FormResultModel>> AssignHorsePoints(List<AlgorithmSettingsEntity> settings, RaceEntity race, DistanceGroupModel distanceGroup, GoingGroupModel goingGroup, decimal distance)
        {
            var result = new List<FormResultModel>();

            return result;
        }

        /// <summary>
        /// Performance in last 2 races
        /// Time since last ran race (TO ANALYSE OPTIMAL)
        /// Age (TO ANALYSE OPTIMAL)
        /// </summary>
        /// <returns></returns>
        private async Task<List<int>> GetCurrentCondition(RaceEntity race) 
        {
            var result = new List<int>();

            return result;
        }

        /// <summary>
        /// Finishing Position in Past races
        /// Normalized times of Past races
        /// </summary>
        /// <returns></returns>
        private async Task<List<int>> GetPastPerformance(RaceEntity race)
        {
            var result = new List<int>();

            return result;
        }

        /// <summary>
        /// Strength of competition in previous races (RPR/AVG Finishing Pos?)
        /// Weight Carried in past races (Optimal)
        /// Jockeys Contribution to last races (performs well with this jockey? +1, performs well regardless of jockey? +1, has not placed with this jockey? 0)
        /// </summary>
        /// <returns></returns>
        private async Task<List<int>> GetAdjustmentsPastPerformance(RaceEntity race)
        {
            var result = new List<int>();

            return result;
        }

        /// <summary>
        /// Weight to be carried
        /// Todays Jockeys ability
        /// </summary>
        /// <returns></returns>
        private async Task<List<int>> GetPresentRaceFactors(RaceEntity race)
        {
            var result = new List<int>();

            return result;
        }

        /// <summary>
        /// Distance Preference
        /// Surface Preference
        /// Condition of surface preference
        /// Specific track preference
        /// </summary>
        /// <returns></returns>
        private async Task<List<int>> GetHorsePreferences(RaceEntity race)
        {
            var result = new List<int>();

            return result;
        }
    }
}
