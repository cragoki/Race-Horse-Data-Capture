using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Models.GetRace;
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
        private readonly IDbContextData _context;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public BentnersModel(IConfigurationRepository configRepository, IMappingTableRepository mappingRepository, IAlgorithmRepository algorithmRepository, IDbContextData context)
        {
            _configRepository = configRepository;
            _mappingRepository = mappingRepository;
            _algorithmRepository = algorithmRepository;
            _context = context;
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
            var horsepoints = await GetHorsePoints(race.RaceHorses, race, settings);
            //TURN THIS INTO List<FormResultModel>
            return result;
        }

        public async Task<List<HorsePredictionModel>> GetHorsePoints(List<RaceHorseEntity> horses, RaceEntity race, List<AlgorithmSettingsEntity> settings) 
        {
            var result = new List<HorsePredictionModel>();

            foreach (var horse in horses)
            {
                var toAdd = new HorsePredictionModel();
                toAdd.points = 0;
                toAdd.horse_id = horse.horse_id;
                //Get past races for horse
                var races = horse.Horse.Races.Where(x => x.Race.Event.created < race.Event.created).ToList();

                if (races.Count() == 0)
                {
                    result.Add(toAdd);
                    continue;
                }

                //Current Condition
                toAdd.points += await GetCurrentCondition(race, horse.Horse, settings);
                //TODO, COULD DO WITH STORING THESE IN THE DESCRIPTION

                result.Add(toAdd);
            }

            return result;
        }


        /// <summary>
        /// Performance in last 2 races
        /// Time since last ran race (TO ANALYSE OPTIMAL)
        /// Age (TO ANALYSE OPTIMAL)
        /// SHOULD WE BE LOOKING AT TB_EVENT RACE TYPE VARIABLE HERE???? JUMPS/NOT JUMPS
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetCurrentCondition(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings) 
        {
            decimal result = 0M;
            var reliabilityCurrentCondition = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityCurrentCondition.ToString()).FirstOrDefault().setting_value.ToString());
            var distances = _mappingRepository.GetDistanceTypes();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();

            //Performance in last 2 races. 0.5 for a single place 0.75 for 2 places, 1 for a place and a win, 1.5 for two wins
            var lastTwo = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.Race.race_class <= race.race_class && distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0)).OrderByDescending(x => x.Race.Event.created).Take(2);
            int placed = 0;
            int won = 0;
            foreach (var lastRace in lastTwo)
            {
                var placePosition = SharedCalculations.GetTake(lastRace.Race.no_of_horses ?? 0);
                if (lastRace.position == 1)
                {
                    won++;
                }
                else if (lastRace.position <= placePosition) 
                {
                    placed++;
                }
            }

            if (placed == 2 || (placed == 0 && won == 1))
            {
                result += 0.75M;
            }
            else if (placed == 1 && won == 0)
            {
                result += 0.5M;
            }
            else if (placed == 1 && won == 1) 
            {
                result += 1M;
            }
            else if (won == 2)
            {
                result += 1.5M;
            }

            //Get the most similar time since last ran race. if won: +0.5, if placed +0.25, if did not place -0.5
            //would be a query like,
            //get the time since last race for this current race
            //get a dictionary of raceIds and time since last race for all of the horses races. 
            //Find the closest period of time to this race and do the position check, keep in mind that 0 position will be included and should not be given points


            //We find that a typical horse's peak racing age is 4.45 years. The rate of improvement from age 2 to 4 1/2 is greater than the rate of decline after age 4 1/2. 
            //so Past peak age by 1-2? -0.25 by 2+? -0.5 <- WRITE QUERY FOR THIS TO VALIDATE
            return result;
        }

        /// <summary>
        /// Finishing Position in Past races
        /// Normalized times of Past races TODO
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetPastPerformance(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
        {
            var result = 0;
            var reliabilityPastPerformance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPastPerformance.ToString()).FirstOrDefault().setting_value.ToString());

            //Number of races placed vs number of races not placed. 75% + placed = +2, 60%+ = +1, 50%+ = +0.5, 0% = -2

            return result;
        }

        /// <summary>
        /// Strength of competition in previous races (RPR/AVG Finishing Pos?)
        /// Weight Carried in past races (Optimal)
        /// Jockeys Contribution to last races (performs well with this jockey? +1, performs well regardless of jockey? +1, has not placed with this jockey? 0)
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetAdjustmentsPastPerformance(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
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
        public async Task<decimal> GetPresentRaceFactors(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
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
        public async Task<decimal> GetHorsePreferences(RaceEntity race, int horseId, List<AlgorithmSettingsEntity> settings)
        {
            var result = 0;
            var reliabilityHorsePreferences = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityHorsePreferences.ToString()).FirstOrDefault().setting_value.ToString());
            //For Specific Track, try to check the tb_course all weather boolean and the Meeting type (both from tb_event)
            return result;
        }
    }
}
