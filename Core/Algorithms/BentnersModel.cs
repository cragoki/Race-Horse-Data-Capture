using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Models.Algorithm.Bentners;
using Core.Models.GetRace;
using Core.Variables;
using Infrastructure.PunterAdmin.ViewModels;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
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
                var tracker = new RaceHorseStatisticsTracker();
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
                var currentCondition = await GetCurrentCondition(race, horse.Horse, settings, tracker);
                toAdd.points += currentCondition.TotalPointsForGetCurrentCondition;
                //TODO, COULD DO WITH STORING TRACKER IN DB
                //REMEMBER TO SET TOTAL POINTS ON TRACKER

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
        public async Task<RaceHorseStatisticsTracker> GetCurrentCondition(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker) 
        {
            decimal result = 0M;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityCurrentCondition = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityCurrentCondition.ToString()).FirstOrDefault().setting_value.ToString());
            

            //Performance in last 2 races. 0.5 for a single place 0.75 for 2 places, 1 for a place and a win, 1.5 for two wins
            var lastTwo = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id).OrderByDescending(x => x.Race.Event.created).Take(2);
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

            //CONSIDERATION : I THINK IF THERE ARE 2 HORSES. HORSE 1: WON LAST RACE, CAME SECOND IN RACE BEFORE. HORSE 2: CAME SECOND IN LAST RACE AND WON THE RACE BEFORE. HORSE 1 SHOULD BE AHEAD HERE BUT GIVEN CURRENT LOGIC THIS WILL NOT BE THE CASE
            if (placed == 2 || (placed == 0 && won == 1))
            {
                result += 0.75M;
                tracker.GetCurrentConditionDescription += "--Plus 0.75 for 2 placed or 1 won--";
            }
            else if (placed == 1 && won == 0)
            {
                result += 0.5M;
                tracker.GetCurrentConditionDescription += "--Plus 0.5 for 1 placed--";
            }
            else if (placed == 1 && won == 1) 
            {
                result += 1.25M;
                tracker.GetCurrentConditionDescription += "--Plus 1.25 for 1 placed and 1 won--";
            }
            else if (won == 2)
            {
                result += 1.75M;
                tracker.GetCurrentConditionDescription += "--Plus 1.75 for 2 won--";
            }

            tracker.TotalPointsForLastTwoRaces = result;

            //Get the most similar time since last ran race. if won: +0.5, if placed +0.25, if did not place -0.25
            //would be a query like,
            //get the time since last race for this current race (within a year)
            var racesOrdered = horse.Races.Where(x => x.position != 0 
                && x.race_id != race.race_id 
                && x.Race.Event.created >= race.Event.created.AddYears(-1))
                .OrderByDescending(x => x.Race.Event.created).ToList();

            if (racesOrdered != null && racesOrdered.Count() > 0) 
            {
                var currentRaceDate = race.Event.created;
                var previousRaceDate = racesOrdered.FirstOrDefault().Race.Event.created;
                var daysSinceLastRace = (currentRaceDate - previousRaceDate).TotalDays;

                var historicDaysSinceLastRace = new List<DaysSinceLastRaceModel>();
                //get a dictionary of raceIds and time since last race for all of the horses races. 
                for (int i = 0; i < racesOrdered.ToList().Count; i++)
                {
                    var r1 = racesOrdered.Skip(i).FirstOrDefault();
                    var r2 = racesOrdered.Skip(i + 1).FirstOrDefault();

                    if (r1 != null && r2 != null)
                    {
                        historicDaysSinceLastRace.Add(new DaysSinceLastRaceModel()
                        {
                            RaceId = r1.race_id,
                            DaysSinceLastRace = Convert.ToInt32((r1.Race.Event.created - r2.Race.Event.created).TotalDays)
                        });
                    }
                }

                if (historicDaysSinceLastRace.Count() > 1)
                {
                    //Find the closest period of time to this race and do the position check
                    var closest = historicDaysSinceLastRace.Aggregate((x, y) => Math.Abs(x.DaysSinceLastRace - Convert.ToInt32(daysSinceLastRace)) < Math.Abs(y.DaysSinceLastRace - Convert.ToInt32(daysSinceLastRace)) ? x : y);
                    var closestRace = racesOrdered.Where(x => x.race_id == closest.RaceId).FirstOrDefault();
                    var pPos = SharedCalculations.GetTake(closestRace.Race.no_of_horses ?? 0);

                    if (closestRace.position == 1)
                    {
                        result += 0.25M;
                        tracker.TotalPointsForTimeSinceLastRace = 0.25M;
                        tracker.GetCurrentConditionDescription += $"--Plus 0.25 for a WIN on time since last race. Current = {daysSinceLastRace}. Previous = {closest.DaysSinceLastRace}--";
                    }
                    else if (closestRace.position <= pPos)
                    {
                        result += 0.10M;
                        tracker.TotalPointsForTimeSinceLastRace = 0.10M;
                        tracker.GetCurrentConditionDescription += $"--Plus 0.10 for a PLACE on time since last race. Current = {daysSinceLastRace}. Previous = {closest.DaysSinceLastRace}--";
                    }
                    else
                    {
                        result -= 0.10M;
                        tracker.TotalPointsForTimeSinceLastRace = -0.10M;
                        tracker.GetCurrentConditionDescription += $"--Minus 0.10 for NO PLACE on time since last race. Current = {daysSinceLastRace}. Previous = {closest.DaysSinceLastRace}--";
                    }
                }
            }

            //We find that a typical horse's peak racing age is 4.45 years. The rate of improvement from age 2 to 4 1/2 is greater than the rate of decline after age 4 1/2. 
            //so Past peak age by 1-2? -0.25 by 2+? -0.5 <- WRITE QUERY FOR THIS TO VALIDATE
            tracker.TotalPointsForGetCurrentCondition = result;
            return tracker;
        }

        /// <summary>
        /// Finishing Position in Past races
        /// Normalized times of Past races TODO
        /// Performance at class
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetPastPerformance(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker)
        {
            var result = 0M;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityPastPerformance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPastPerformance.ToString()).FirstOrDefault().setting_value.ToString());
            var distances = _mappingRepository.GetDistanceTypes();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var pastRaces = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id && distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && x.Race.race_class <= race.race_class).OrderByDescending(x => x.Race.Event.created);

            //Number of races placed vs number of races not placed. 75% + placed = +2, 60%+ = +1, 50%+ = +0.5, 0% = -2
            foreach (var pastRace in pastRaces)
            {
                //Reduce this value slightly for each iteration
                var pointsForWin = 1M;
                var pointsForPlace = pointsForWin - 0.25M;
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                //Check if class is lower and if it is, multiply points to add
                var difference = race.race_class - pastRace.Race.race_class;
                var addition = 0.25M * difference;
                pointsForWin += addition ?? 0;

                if (pastRace.position == 1)
                {
                    tracker.TotalPoints += pointsForWin;
                    tracker.GetPastPerformanceDescription += $"--Plus {pointsForWin} for win at race class {pastRace.Race.race_class}--";
                    result += pointsForWin;
                }
                else if (pastRace.position <= placePosition)
                {
                    tracker.TotalPoints += pointsForPlace;
                    tracker.GetPastPerformanceDescription += $"--Plus {pointsForPlace} for place at race class {pastRace.Race.race_class}--";
                    result += pointsForPlace;
                }

            }

            tracker.TotalPointsForPastPerformance = result;
            return tracker;
        }

        /// <summary>
        /// Strength of competition in previous races (RPR/AVG Finishing Pos?)
        /// Weight Carried in past races (Optimal)
        /// Jockeys Contribution to last races (performs well with this jockey? +1, performs well regardless of jockey? +1, has not placed with this jockey? 0)
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetAdjustmentsPastPerformance(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker)
        {
            var result = 0M;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityAdjustmentsPastPerformance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityAdjustmentsPastPerformance.ToString()).FirstOrDefault().setting_value.ToString());
            var distances = _mappingRepository.GetDistanceTypes();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var pastRaces = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id && distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0)).OrderByDescending(x => x.Race.Event.created);
            var raceHorse = race.RaceHorses.Where(x => x.horse_id == horse.horse_id).FirstOrDefault();
            //Strength of competition in past races (can either look at RPR (v1) BUT INVESTIGATE TO SEE WHAT IS A GOOD RPR??
            //But what RPRs don’t do is tell you what types of weight a horse has carried when posting its best ratings. Many horses win handicaps when given a chance to carry a lightweight for the first time, while others are better at carrying big weights against lower-class opponents.
            //What Else Don’t RPRs Tell You?
            //RPRs also won’t tell you when a trainer is in form or when a horse is reunited with a jockey that has won it before.
            //So Higher the better, could do it as % of horses in past races where RPR > horses RPR. 
            //We would even need to look deeper into this. Horse won last race but all horses had a lower RPR, this should not award as many points as when a horse has raced against 50% field of higher rprs. could get tricky.
            //So to keep this simple for V1, we get the average rpr for each race, and if it is greater than the horses rpr for that race we increment a variable by one. 
            var pointsForStrength = 0.5M;
            var racesWithGreaterRprs = 0;
            foreach (var pastRace in pastRaces) 
            {
                var horseForThisRace = pastRace.Race.RaceHorses.Where(x => x.horse_id == horse.horse_id).FirstOrDefault();
                var horseForThisRaceRpr = ConfigureRPR(horseForThisRace, pastRace.Race.Event.created) ?? 0;

                var allRprs = new List<int>();
                var averages = new List<double>();
                foreach (var pastHorseForRace in pastRace.Race.RaceHorses.Where(x => x.horse_id != horse.horse_id)) 
                {
                    var rpr = ConfigureRPR(pastHorseForRace, pastRace.Race.Event.created) ?? 0;
                    allRprs.Add(rpr);
                }

                averages.Add(allRprs.Where(x => x != 0).Average());

                if (averages.Average() > horseForThisRaceRpr) 
                {
                    racesWithGreaterRprs = racesWithGreaterRprs + 1;
                }
            }
            //we then take that variable and work it out as a % against total races. Divide that by 10 and multiply it by points
            var percentageOfRacesWithGreaterRpr = (int)Math.Round((double)(100 * racesWithGreaterRprs) / pastRaces.Count());
            result += (percentageOfRacesWithGreaterRpr / 10) * pointsForStrength; //TEST THIS WORKS
            tracker.TotalPointsForStrengthOfCompetition += (percentageOfRacesWithGreaterRpr / 10) * pointsForStrength;
            tracker.GetPastPerformanceAdjustmentsDescription += $"--Plus {tracker.TotalPointsForStrengthOfCompetition} for strength of competition with {percentageOfRacesWithGreaterRpr}% of horses with greater rpr --";
            //Optimal Weight in past races - Get each past race and get the weight for their top 3 performances
            //May need some sort of diff value (ie. if weight = -2 compared to previous race, +x if -1 +y etc...)
            var pointsForWeight = 0.5M;
            var topHalf = pastRaces.OrderBy(x => x.position).Take(pastRaces.Count() / 2).ToList();
            var averageWeight = topHalf.Average(x => Decimal.Parse(x.weight.Replace(" ", "")));
            //we can do this as a reduction. if weight diff is 0.6, multiply pointsForWeight by that value and remove it from total
            var diff = Math.Abs(averageWeight - Decimal.Parse(raceHorse.weight.Replace(" ", ""))); // TO TEST THIS DOESNT RETURN NEGATIVE NUMBERS
            result -= (pointsForWeight * diff);
            tracker.TotalPointsForWeight -= (pointsForWeight * diff);
            tracker.GetPastPerformanceAdjustmentsDescription += $"--Minus {tracker.TotalPointsForWeight} for weight adjustment, peak weight of {averageWeight}--";

            //Has raced with jockey before? If so how has that gone? (performs well with this jockey?)
            //So if the horse has 4 races with this jockey and has placed or won twice. Add pointsForJockey * 0.5
            var pointsForJockey = 1M;
            var racesWithJockey = pastRaces.Where(x => x.jockey_id == raceHorse.jockey_id).ToList();
            int placedWithJockey = 0;

            if (racesWithJockey.Count() > 0)
            {
                foreach (var raceWithJockey in racesWithJockey)
                {
                    var placePosition = SharedCalculations.GetTake(raceWithJockey.Race.no_of_horses ?? 0);
                    if (raceWithJockey.position == 1 || raceWithJockey.position <= placePosition)
                    {
                        placedWithJockey = placedWithJockey + 1;
                    }
                }
                var multiplier = (placedWithJockey / racesWithJockey.Count());
                result += (pointsForJockey * multiplier);
                tracker.TotalPointsForJockeyContribution += (pointsForJockey * multiplier);
                tracker.GetPastPerformanceAdjustmentsDescription += $"--plus {tracker.TotalPointsForJockeyContribution} for jockey contribution. placed {placedWithJockey} times with jockey --";
            }

            tracker.TotalPoints += result;
            return tracker;
        }

        /// <summary>
        /// Weight to be carried (Research Weight contribution towards a race)
        /// Todays Jockeys ability
        /// Trainer form
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetPresentRaceFactors(RaceEntity race, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker)
        {
            //SHOULD ONLY BE CALLED ONCE, THIS WILL ANALYSE ALL JOCKEYS AND TRAINERS INVOLVED IN THIS RACE AND RANK THEM, POINTS ARE TO BE ASSIGNED OUTSIDE OF THIS METHOD
            var result = 0;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityPresentRaceFactors = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPresentRaceFactors.ToString()).FirstOrDefault().setting_value.ToString());
            tracker.pointsForJockey = reliabilityPresentRaceFactors / 2;
            tracker.pointsForTrainer = reliabilityPresentRaceFactors / 2;
            tracker.TrainerRankings = new Dictionary<int, double>();
            tracker.JockeyRankings = new Dictionary<int, double>();


            foreach (var raceHorse in race.RaceHorses)
            {
                var jockey = raceHorse.jockey_id;
                //Get jockey history for the last 6 months
                var jockeyHistory = _context.tb_race_horse.Where(x => x.jockey_id == jockey && x.Race.Event.created > x.Race.Event.created.AddMonths(-6) && x.position != 0).OrderByDescending(x => x.Race.Event.created).ToList();
                tracker.JockeyRankings.Add(jockey, jockeyHistory.Average(x => x.position));

                var trainer = raceHorse.trainer_id;
                //Get trainer history for the last 6 months
                var trainerHistory = _context.tb_race_horse.Where(x => x.trainer_id == trainer && x.Race.Event.created > x.Race.Event.created.AddMonths(-6) && x.position != 0).OrderByDescending(x => x.Race.Event.created).ToList();
                tracker.TrainerRankings.Add(trainer, trainerHistory.Average(x => x.position));
            }

            return tracker;
        }

        /// <summary>
        /// Distance Preference
        /// Surface Preference
        /// Jumps/Not Jumps
        /// Condition of surface preference
        /// Specific track preference
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetHorsePreferences(RaceEntity race, HorseEntity horse, List<AlgorithmSettingsEntity> settings, RaceHorseStatisticsTracker tracker)
        {
            var result = 0;
            var distances = _mappingRepository.GetDistanceTypes();
            var goings = _mappingRepository.GetGoingTypes();

            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityHorsePreferences = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityHorsePreferences.ToString()).FirstOrDefault().setting_value.ToString());
            var pointsForEachCondition = reliabilityHorsePreferences / 4;

            //For Specific Track, try to check the tb_course all weather boolean and the Meeting type (both from tb_event)
            var racesAtCourse = _context.tb_race_horse.Where(x => x.horse_id == horse.horse_id && x.Race.Event.course_id == race.Event.course_id && x.position != 0).ToList();
            foreach (var condition1 in racesAtCourse) 
            {
                var placePosition = SharedCalculations.GetTake(condition1.Race.no_of_horses ?? 0);

                if (condition1.position == 1)
                {

                }
                else if (condition1.position <= placePosition)
                {

                }
            }
            //Get Horses Distance group they have performed best at... (Ie: 50% places at distance +1, 75% places at distance +2)
            var racesAtDistanceGroup = _context.tb_race_horse.Where(x => x.horse_id == horse.horse_id && distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && x.position != 0).ToList();
            foreach (var condition2 in racesAtDistanceGroup)
            {
                var placePosition = SharedCalculations.GetTake(condition2.Race.no_of_horses ?? 0);

                if (condition2.position == 1)
                {

                }
                else if (condition2.position <= placePosition)
                {

                }
            }
            //Get Horses Jumps/Not Jumps they have performed best at
            if (race.Event.meeting_type != null) 
            {
                var racesAtRaceType = _context.tb_race_horse.Where(x => x.horse_id == horse.horse_id && x.Race.Event.meeting_type != null && x.Race.Event.meeting_type == race.Event.meeting_type && x.position != 0).ToList();
                foreach (var condition3 in racesAtRaceType)
                {
                    var placePosition = SharedCalculations.GetTake(condition3.Race.no_of_horses ?? 0);

                    if (condition3.position == 1)
                    {

                    }
                    else if (condition3.position <= placePosition)
                    {

                    }
                }
            }

            //Get Horses Going group they have performed best at... (Ie: 50% places at Going +1, 75% places at Going +2)
            var racesAtGoingGroup = _context.tb_race_horse.Where(x => x.horse_id == horse.horse_id && goingGroups.ElementIds.Contains(x.Race.going ?? 0) && x.position != 0).ToList();
            foreach (var condition4 in racesAtGoingGroup)
            {
                var placePosition = SharedCalculations.GetTake(condition4.Race.no_of_horses ?? 0);

                if (condition4.position == 1)
                {

                }
                else if (condition4.position <= placePosition)
                {

                }
            }

            return tracker;
        }

        private int? ConfigureRPR(RaceHorseEntity raceHorse, DateTime created)
        {
            var result = 0;

            if (raceHorse.Horse.Archive != null && raceHorse.Horse.Archive.Count() != 0)
            {
                var archive = raceHorse.Horse.Archive;
                var rprString = archive.Where(x => x.field_changed == "rpr" && x.date < created)
                    .OrderByDescending(x => x.date).FirstOrDefault()?.new_value;

                if (rprString != "-")
                {
                    if (Int32.TryParse(rprString, out var rprInt))
                    {
                        result = rprInt;
                    }
                }
            }

            return result;
        }
    }
}
