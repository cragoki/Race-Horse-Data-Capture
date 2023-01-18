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

        public async Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races)
        {
            var result = new AlgorithmResult();
            try
            {
                List<double> runningTotal = new List<double>();
                int raceCounter = 0;

                foreach (var race in races)
                {
                    var predictionResult = await RunModel(race);

                    if (predictionResult.Count() == 0)
                    {
                        Console.WriteLine($"Valid Prediction with {predictionResult}%");
                        raceCounter++;
                        runningTotal.Add(0);
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

        public async Task<List<FormResultModel>> RunModel(RaceEntity race)
        {
            var result = new List<FormResultModel>();

            if (race.RaceHorses.All(x => x.position == 0))
            {
                return result;
            }

            //Variables
            var settings = _configRepository.GetAlgorithmSettings((int)AlgorithmEnum.BentnersModel);
            var placedRange = SharedCalculations.GetTake(race.no_of_horses ?? 0);
            var counter = 0;
            var horses = race.RaceHorses;
            var listOfHorses = new List<HorseEntity>();
            var distances = _mappingRepository.GetDistanceTypes();
            var goings = _mappingRepository.GetGoingTypes();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            //Settings


            //BEGIN CALCULATING PREDICTED RESULTS
            var horsepoints = new List<HorsePredictionModel>();
            foreach (var horse in race.RaceHorses)
            {
                var toAdd = new HorsePredictionModel();
                toAdd.points = 0;
                toAdd.horse_id = horse.horse_id;
                //Get past races for horse
                var races = horse.Horse.Races.Where(x => x.Race.Event.created < race.Event.created).ToList();

                if (races.Count() == 0)
                {
                    horsepoints.Add(toAdd);
                    continue;
                }

                //Current Condition
                toAdd.points += await GetCurrentCondition(race, horse.horse_id, settings);
            }

                return result;
        }


        /// <summary>
        /// Performance in last 2 races
        /// Time since last ran race (TO ANALYSE OPTIMAL)
        /// Age (TO ANALYSE OPTIMAL)
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetCurrentCondition(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings) 
        {
            var result = 0;
            var reliabilityCurrentCondition = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityCurrentCondition.ToString()).FirstOrDefault().setting_value.ToString());


            return result;
        }

        /// <summary>
        /// Finishing Position in Past races
        /// Normalized times of Past races
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetPastPerformance(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
        {
            var result = 0;
            var reliabilityPastPerformance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPastPerformance.ToString()).FirstOrDefault().setting_value.ToString());

            return result;
        }

        /// <summary>
        /// Strength of competition in previous races (RPR/AVG Finishing Pos?)
        /// Weight Carried in past races (Optimal)
        /// Jockeys Contribution to last races (performs well with this jockey? +1, performs well regardless of jockey? +1, has not placed with this jockey? 0)
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetAdjustmentsPastPerformance(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
        {
            var result = 0;
            var reliabilityAdjustmentsPastPerformance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityAdjustmentsPastPerformance.ToString()).FirstOrDefault().setting_value.ToString());

            return result;
        }

        /// <summary>
        /// Weight to be carried
        /// Todays Jockeys ability
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetPresentRaceFactors(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
        {
            var result = 0;
            var reliabilityPresentRaceFactors = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPresentRaceFactors.ToString()).FirstOrDefault().setting_value.ToString());

            return result;
        }

        /// <summary>
        /// Distance Preference
        /// Surface Preference
        /// Condition of surface preference
        /// Specific track preference
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetHorsePreferences(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
        {
            var result = 0;
            var reliabilityHorsePreferences = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityHorsePreferences.ToString()).FirstOrDefault().setting_value.ToString());

            return result;
        }
    }
}
