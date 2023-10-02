using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Models.Algorithm;
using Core.Models.Algorithm.Bentners;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algorithms
{
    public class BentnersModel : IBentnersModel
    {

        private readonly IDbContextData _context;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public BentnersModel(IDbContextData context)
        {
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

            try
            {
                //BEGIN CALCULATING PREDICTED RESULTS
                var horsePoints = await GetHorsePoints(race.RaceHorses, race);
                //TURN THIS INTO List<FormResultModel>
                foreach (var horsePoint in horsePoints)
                {
                    result.Add(new FormResultModel()
                    {
                        horse_id = horsePoint.horse_id,
                        RaceHorseId = horsePoint.race_horse_id,
                        Predictability = "",
                        Points = horsePoint.points,
                        PointsDescription = "",
                        Tracker = horsePoint.tracker
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<List<HorsePredictionModel>> GetHorsePoints(List<RaceHorseEntity> horses, RaceEntity race)
        {
            var result = new List<HorsePredictionModel>();
            var raceTracker = new RaceHorseStatisticsTracker();
            var presentRaceFactors = await GetPresentRaceFactors(race, raceTracker);
            var topJockeys = raceTracker.JockeyRankings.OrderBy(x => x.Value).Take(2); 
            var topTrainers = raceTracker.JockeyRankings.OrderBy(x => x.Value).Take(2);
            foreach (var horse in horses)
            {
                var tracker = new RaceHorseStatisticsTracker();
                var toAdd = new HorsePredictionModel();
                toAdd.points = 0;
                toAdd.horse_id = horse.horse_id;
                toAdd.race_horse_id = horse.race_horse_id;
                //Get past races for horse
                var races = horse.Horse.Races.Where(x => x.Race.Event.created < race.Event.created);

                if (races.Count() == 0)
                {
                    result.Add(toAdd);
                    continue;
                }

                if (topJockeys.Select(x => x.Key).Contains(horse.jockey_id))
                {
                    tracker.PointsGivenForJockey = presentRaceFactors.pointsForJockey;
                    tracker.GetPresentRaceFactorsDescription += $"--Given {presentRaceFactors.pointsForJockey} points for favoured Jockey--";
                    tracker.TotalPoints += FormatHelper.ToTwoPlaces(presentRaceFactors.pointsForJockey);
                }
                if (topTrainers.Select(x => x.Key).Contains(horse.trainer_id))
                {
                    tracker.PointsGivenForTrainer = presentRaceFactors.pointsForTrainer;
                    tracker.GetPresentRaceFactorsDescription += $"--Given {presentRaceFactors.pointsForTrainer} points for favoured Trainer--";
                    tracker.TotalPoints += FormatHelper.ToTwoPlaces(presentRaceFactors.pointsForJockey);
                }
                //Current Condition
                await GetCurrentCondition(race, horse.Horse, tracker);
                await GetPastPerformance(race, horse.Horse, tracker);
                await GetAdjustmentsPastPerformance(race, horse.Horse, tracker);
                await GetHorsePreferences(race, horse.Horse, tracker);

                toAdd.points = tracker.TotalPoints;
                //TODO, COULD DO WITH STORING TRACKER IN DB
                //FIRST CHECK THAT ANY TRACKERS FOR THIS RACE_HORSE_ID HAS NOT ALREADY BEEN STORED, IF IT HAS IGNORE IT...
                var existingAlgorithmTracker = _context.tb_algorithm_tracker.Where(x => x.race_horse_id == horse.race_horse_id && x.total_points == tracker.TotalPoints).FirstOrDefault();
                var algorithm = _context.tb_algorithm.Where(x => x.algorithm_name == "BentersModel").FirstOrDefault();
                if (existingAlgorithmTracker == null)
                {
                    toAdd.tracker = new AlgorithmTrackerEntity()
                    {
                        total_points = tracker.TotalPoints,
                        total_points_for_adjustments_past_performance = tracker.TotalPointsForAdjustmentsPastPerformance,
                        total_points_for_distance = tracker.TotalPointsForDistance,
                        total_points_for_get_current_condition = tracker.TotalPointsForGetCurrentCondition,
                        total_points_for_going = tracker.TotalPointsForGoing,
                        total_points_for_jockey_contribution = tracker.TotalPointsForJockeyContribution,
                        total_points_for_last_two_races = tracker.TotalPointsForLastTwoRaces,
                        total_points_for_past_performance = tracker.TotalPointsForPastPerformance,
                        total_points_for_race_type = tracker.TotalPointsForRaceType,
                        total_points_for_specific_track = tracker.TotalPointsForSpecificTrack,
                        total_points_for_strength_of_competition = tracker.TotalPointsForStrengthOfCompetition,
                        total_points_for_time_since_last_race = tracker.TotalPointsForTimeSinceLastRace,
                        total_points_for_weight = tracker.TotalPointsForWeight,
                        algorithm_id = algorithm.algorithm_id,
                        get_present_race_factors_description = tracker.GetPresentRaceFactorsDescription,
                        created = DateTime.Now,
                        get_current_condition_description = tracker.GetCurrentConditionDescription,
                        get_horse_preferences_description = tracker.GetHorsePreferencesDescription,
                        get_past_performance_adjustments_description = tracker.GetPastPerformanceAdjustmentsDescription,
                        get_past_performance_description = tracker.GetPastPerformanceDescription,
                        points_given_for_jockey = tracker.PointsGivenForJockey,
                        points_given_for_trainer = tracker.PointsGivenForTrainer,
                        race_horse_id = horse.race_horse_id
                    };
                }

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
        public async Task<RaceHorseStatisticsTracker> GetCurrentCondition(RaceEntity race, HorseEntity horse, RaceHorseStatisticsTracker tracker)
        {
            decimal result = 0M;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityCurrentCondition = _context.tb_algorithm_settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityCurrentCondition.ToString()).First().setting_value.ToString();

            //Performance in last 2 races. 0.5 for a single place 0.75 for 2 places, 1 for a place and a win, 1.5 for two wins
            var lastTwo = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id && x.Race.race_class <= race.race_class).OrderByDescending(x => x.Race.Event.created).Take(2);
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

            tracker.TotalPointsForLastTwoRaces = FormatHelper.ToTwoPlaces(result);

            //Get the most similar time since last ran race. if won: +0.5, if placed +0.25, if did not place -0.25
            //would be a query like,
            //get the time since last race for this current race (within a year)
            var racesOrdered = horse.Races.Where(x => x.position != 0
                && x.race_id != race.race_id
                && x.Race.Event.created >= race.Event.created.AddYears(-1))
                .OrderByDescending(x => x.Race.Event.created);

            if (racesOrdered != null && racesOrdered.Count() > 0)
            {
                var currentRaceDate = race.Event.created;
                var previousRaceDate = racesOrdered.FirstOrDefault().Race.Event.created;
                var daysSinceLastRace = (currentRaceDate - previousRaceDate).TotalDays;

                var historicDaysSinceLastRace = new List<DaysSinceLastRaceModel>();
                //get a dictionary of raceIds and time since last race for all of the horses races. 
                for (int i = 0; i < racesOrdered.Count(); i++)
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
            tracker.TotalPointsForGetCurrentCondition = FormatHelper.ToTwoPlaces(result);
            tracker.TotalPoints += FormatHelper.ToTwoPlaces(result);



            return tracker;
        }

        /// <summary>
        /// Finishing Position in Past races
        /// Normalized times of Past races TODO
        /// Performance at class
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetPastPerformance(RaceEntity race, HorseEntity horse, RaceHorseStatisticsTracker tracker)
        {
            var result = 0M;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityPastPerformance = _context.tb_algorithm_settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPastPerformance.ToString()).First().setting_value.ToString();
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();

            if (distanceGroups == null)
            {
                return tracker;
            }
            var pastRaces = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id && distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && x.Race.race_class <= race.race_class).OrderByDescending(x => x.Race.Event.created).Take(3);

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
                    tracker.GetPastPerformanceDescription += $"--Plus {pointsForWin} for win at race class {pastRace.Race.race_class}--";
                    result += pointsForWin;
                }
                else if (pastRace.position <= placePosition)
                {
                    tracker.GetPastPerformanceDescription += $"--Plus {pointsForPlace} for place at race class {pastRace.Race.race_class}--";
                    result += pointsForPlace;
                }

            }

            tracker.TotalPointsForPastPerformance = FormatHelper.ToTwoPlaces(result);
            tracker.TotalPoints += FormatHelper.ToTwoPlaces(result);
            return tracker;
        }

        /// <summary>
        /// Strength of competition in previous races (RPR/AVG Finishing Pos?)
        /// Weight Carried in past races (Optimal)
        /// Jockeys Contribution to last races (performs well with this jockey? +1, performs well regardless of jockey? +1, has not placed with this jockey? 0)
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetAdjustmentsPastPerformance(RaceEntity race, HorseEntity horse, RaceHorseStatisticsTracker tracker)
        {
            var result = 0M;
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityPastPerformance = _context.tb_algorithm_settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityAdjustmentsPastPerformance.ToString()).First().setting_value.ToString();

            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            if (distanceGroups == null)
            {
                return tracker;
            }
            var pastRaces = horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id && distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0)).OrderByDescending(x => x.Race.Event.created).Take(3);
            var raceHorse = race.RaceHorses.Where(x => x.horse_id == horse.horse_id).FirstOrDefault();
            //Strength of competition in past races (can either look at RPR (v1) BUT INVESTIGATE TO SEE WHAT IS A GOOD RPR??
            //But what RPRs don’t do is tell you what types of weight a horse has carried when posting its best ratings. Many horses win handicaps when given a chance to carry a lightweight for the first time, while others are better at carrying big weights against lower-class opponents.
            //What Else Don’t RPRs Tell You?
            //RPRs also won’t tell you when a trainer is in form or when a horse is reunited with a jockey that has won it before.
            //So Higher the better, could do it as % of horses in past races where RPR > horses RPR. 
            //We would even need to look deeper into this. Horse won last race but all horses had a lower RPR, this should not award as many points as when a horse has raced against 50% field of higher rprs. could get tricky.
            //So to keep this simple for V1, we get the average rpr for each race, and if it is greater than the horses rpr for that race we increment a variable by one. 
            //var pointsForStrength = 0.5M;
            //var racesWithGreaterRprs = 0;

            if (pastRaces.Count() > 0)
            {
                //    foreach (var pastRace in pastRaces)
                //    {
                //        var horseForThisRace = pastRace.Race.RaceHorses.Where(x => x.horse_id == horse.horse_id).FirstOrDefault();
                //        var horseForThisRaceRpr = ConfigureRPR(horseForThisRace, pastRace.Race.Event.created) ?? 0;

                //        var allRprs = new List<int>();
                //        var averages = new List<double>();
                //        foreach (var pastHorseForRace in pastRace.Race.RaceHorses.Where(x => x.horse_id != horse.horse_id))
                //        {
                //            var rpr = ConfigureRPR(pastHorseForRace, pastRace.Race.Event.created) ?? 0;
                //            allRprs.Add(rpr);
                //        }

                //        if (allRprs.Where(x => x != 0).Count() > 0)
                //        {
                //            averages.Add(allRprs.Where(x => x != 0).Average());

                //            if (averages.Average() > horseForThisRaceRpr)
                //            {
                //                racesWithGreaterRprs = racesWithGreaterRprs + 1;
                //            }
                //        }
                //    }

                if (pastRaces.Count() > 0)
                {
                    //we then take that variable and work it out as a % against total races. Divide that by 10 and multiply it by points
                    //var percentageOfRacesWithGreaterRpr = (int)Math.Round((double)(100 * racesWithGreaterRprs) / pastRaces.Count());
                    //result += (percentageOfRacesWithGreaterRpr / 10) * pointsForStrength;
                    //tracker.TotalPointsForStrengthOfCompetition += FormatHelper.ToTwoPlaces((percentageOfRacesWithGreaterRpr / 10) * pointsForStrength);
                    //tracker.GetPastPerformanceAdjustmentsDescription += $"--Plus {tracker.TotalPointsForStrengthOfCompetition} for strength of competition with {percentageOfRacesWithGreaterRpr}% of horses with greater rpr --";
                    //Optimal Weight in past races - Get each past race and get the weight for their top 3 performances
                    //May need some sort of diff value (ie. if weight = -2 compared to previous race, +x if -1 +y etc...)
                    var pointsForWeight = 0.5M;
                    var topHalf = pastRaces.OrderBy(x => x.position).Take(pastRaces.Count() / 2);
                    if (topHalf.Count() > 0)
                    {
                        var averageWeight = topHalf.Average(x => Decimal.Parse(x.weight.Replace(" ", "").Replace("\n", "")));
                        //we can do this as a reduction. if weight diff is 0.6, multiply pointsForWeight by that value and remove it from total
                        var diff = Math.Abs(averageWeight - Decimal.Parse(raceHorse.weight.Replace(" ", "").Replace("\n", ""))); // TO TEST THIS DOESNT RETURN NEGATIVE NUMBERS
                        result -= (pointsForWeight * diff);
                        tracker.TotalPointsForWeight -= FormatHelper.ToTwoPlaces((pointsForWeight * diff));
                        tracker.GetPastPerformanceAdjustmentsDescription += $"--Minus {tracker.TotalPointsForWeight} for weight adjustment, peak weight of {FormatHelper.ToTwoPlaces(averageWeight)}--";
                    }

                    //Has raced with jockey before? If so how has that gone? (performs well with this jockey?)
                    //So if the horse has 4 races with this jockey and has placed or won twice. Add pointsForJockey * 0.5
                    var pointsForJockey = 0.5M;
                    var racesWithJockey = pastRaces.Where(x => x.jockey_id == raceHorse.jockey_id);
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
                        decimal multiplier = 0M;

                        if (placedWithJockey > 0)
                        {
                            multiplier = ((decimal)placedWithJockey / (decimal)racesWithJockey.Count());
                        }
                        result += (pointsForJockey * multiplier);
                        tracker.TotalPointsForJockeyContribution += FormatHelper.ToTwoPlaces((pointsForJockey * multiplier));
                        tracker.GetPastPerformanceAdjustmentsDescription += $"--plus {tracker.TotalPointsForJockeyContribution} for jockey contribution. placed {placedWithJockey} times with jockey --";
                    }
                }
            }

            tracker.TotalPointsForAdjustmentsPastPerformance = FormatHelper.ToTwoPlaces(result);
            tracker.TotalPoints += FormatHelper.ToTwoPlaces(result);
            return tracker;
        }

        /// <summary>
        /// Weight to be carried (Research Weight contribution towards a race)
        /// Todays Jockeys ability
        /// Trainer form
        /// </summary>
        /// <returns></returns>
        public async Task<RaceHorseStatisticsTracker> GetPresentRaceFactors(RaceEntity race, RaceHorseStatisticsTracker tracker)
        {
            //SHOULD ONLY BE CALLED ONCE, THIS WILL ANALYSE ALL JOCKEYS AND TRAINERS INVOLVED IN THIS RACE AND RANK THEM, POINTS ARE TO BE ASSIGNED OUTSIDE OF THIS METHOD
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityPresentRaceFactors = _context.tb_algorithm_settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityPresentRaceFactors.ToString()).First().setting_value.ToString();
            tracker.pointsForJockey = Decimal.Parse(reliabilityPresentRaceFactors) / 2;
            tracker.pointsForTrainer = Decimal.Parse(reliabilityPresentRaceFactors) / 2;
            tracker.TrainerRankings = new Dictionary<int, double>();
            tracker.JockeyRankings = new Dictionary<int, double>();


            foreach (var raceHorse in race.RaceHorses)
            {
                var jockey = raceHorse.jockey_id;
                //Get jockey history for the last 6 months
                var jockeyHistory = _context.tb_race_horse.Where(x => x.jockey_id == jockey && x.Race.Event.created > x.Race.Event.created.AddMonths(-6) && x.position != 0).OrderByDescending(x => x.Race.Event.created);
                //Add Count check before adding to tracker
                if (jockeyHistory != null && jockeyHistory.Count() > 0 && !tracker.JockeyRankings.Any(x => x.Key == jockey))
                {
                    tracker.JockeyRankings.Add(jockey, jockeyHistory.Average(x => x.position));
                }

                var trainer = raceHorse.trainer_id;
                //Get trainer history for the last 6 months
                var trainerHistory = _context.tb_race_horse.Where(x => x.trainer_id == trainer && x.Race.Event.created > x.Race.Event.created.AddMonths(-6) && x.position != 0).OrderByDescending(x => x.Race.Event.created);
                if (trainerHistory != null && trainerHistory.Count() > 0 && !tracker.TrainerRankings.Any(x => x.Key == trainer))
                {
                    tracker.TrainerRankings.Add(trainer, trainerHistory.Average(x => x.position));
                }
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
        public async Task<RaceHorseStatisticsTracker> GetHorsePreferences(RaceEntity race, HorseEntity horse, RaceHorseStatisticsTracker tracker)
        {
            var result = 0M;
            var distances = _context.tb_distance_type.AsEnumerable();
            var goings = _context.tb_going_type.AsEnumerable();

            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            if (distanceGroups == null || goingGroups == null)
            {
                return tracker;
            }
            //PLAN FOR V2 IMPLEMENTATION - TO SPLIT THIS UP INTO MULTIPLE VARIABLES SO WE CAN ADJUST THEM
            var reliabilityHorsePreferences = _context.tb_algorithm_settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilityHorsePreferences.ToString()).First().setting_value.ToString();

            var pointsForEachCondition = Decimal.Parse(reliabilityHorsePreferences) / 4;

            //For Specific Track, try to check the tb_course all weather boolean and the Meeting type (both from tb_event)
            var racesAtCourse = horse.Races.Where(x => x.Race.Event.course_id == race.Event.course_id && x.position != 0).Take(3);

            if (racesAtCourse.Count() > 0)
            {
                var pointsForEachC1 = FormatHelper.ToTwoPlaces((pointsForEachCondition / racesAtCourse.Count()));

                foreach (var condition1 in racesAtCourse)
                {
                    var cRace = condition1.Race;
                    var placePosition = SharedCalculations.GetTake(cRace.no_of_horses ?? 0);

                    if (condition1.position == 1)
                    {
                        tracker.TotalPointsForSpecificTrack += FormatHelper.ToTwoPlaces(pointsForEachC1);
                        tracker.GetHorsePreferencesDescription += $"--plus {FormatHelper.ToTwoPlaces(pointsForEachC1)} for winning race at Track--";
                        result += pointsForEachC1;
                    }
                    else if (condition1.position <= placePosition)
                    {
                        tracker.TotalPointsForSpecificTrack += FormatHelper.ToTwoPlaces((pointsForEachC1 / 2));
                        tracker.GetHorsePreferencesDescription += $"--plus {FormatHelper.ToTwoPlaces((pointsForEachC1 / 2))} for placing race at Track--";
                        result += FormatHelper.ToTwoPlaces((pointsForEachC1 / 2));
                    }
                }
            }

            //Get Horses Distance group they have performed best at... (Ie: 50% places at distance +1, 75% places at distance +2)
            var racesAtDistanceGroup = horse.Races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && x.position != 0).Take(3);

            if (racesAtDistanceGroup.Count() > 0)
            {
                var pointsForEachC2 = FormatHelper.ToTwoPlaces((pointsForEachCondition / racesAtDistanceGroup.Count()));

                foreach (var condition2 in racesAtDistanceGroup)
                {
                    var cRace = condition2.Race;
                    var placePosition = SharedCalculations.GetTake(cRace.no_of_horses ?? 0);

                    if (condition2.position == 1)
                    {
                        tracker.TotalPointsForDistance += FormatHelper.ToTwoPlaces(pointsForEachC2);
                        tracker.GetHorsePreferencesDescription += $"--plus {pointsForEachC2} for winning race at Distance--";
                        result += pointsForEachC2;
                    }
                    else if (condition2.position <= placePosition)
                    {
                        tracker.TotalPointsForDistance += FormatHelper.ToTwoPlaces((pointsForEachC2 / 2));
                        tracker.GetHorsePreferencesDescription += $"--plus {FormatHelper.ToTwoPlaces((pointsForEachC2 / 2))} for placing race at Distance--";
                        result += FormatHelper.ToTwoPlaces((pointsForEachC2 / 2));
                    }
                }
            }

            //Get Horses Jumps/Not Jumps they have performed best at
            if (race.Event.meeting_type != null)
            {
                var racesAtRaceType = horse.Races.Where(x => x.Race.Event.meeting_type != null && x.Race.Event.meeting_type == race.Event.meeting_type && x.position != 0).Take(3);

                if (racesAtRaceType.Count() > 0)
                {
                    var pointsForEachC3 = FormatHelper.ToTwoPlaces((pointsForEachCondition / racesAtRaceType.Count()));

                    foreach (var condition3 in racesAtRaceType)
                    {
                        var cRace = condition3.Race;
                        var placePosition = SharedCalculations.GetTake(cRace.no_of_horses ?? 0);

                        if (condition3.position == 1)
                        {
                            tracker.TotalPointsForRaceType += pointsForEachC3;
                            tracker.GetHorsePreferencesDescription += $"--plus {pointsForEachC3} for winning race at Race Type--";
                            result += pointsForEachC3;
                        }
                        else if (condition3.position <= placePosition)
                        {
                            tracker.TotalPointsForRaceType += FormatHelper.ToTwoPlaces((pointsForEachC3 / 2));
                            tracker.GetHorsePreferencesDescription += $"--plus {FormatHelper.ToTwoPlaces((pointsForEachC3 / 2))} for placing race at Race Type--";
                            result += FormatHelper.ToTwoPlaces((pointsForEachC3 / 2));
                        }
                    }
                }
            }

            //Get Horses Going group they have performed best at... (Ie: 50% places at Going +1, 75% places at Going +2)
            var racesAtGoingGroup = horse.Races.Where(x => goingGroups.ElementIds.Contains(x.Race.going ?? 0) && x.position != 0).Take(3);

            if (racesAtGoingGroup.Count() > 0)
            {
                var pointsForEachC4 = FormatHelper.ToTwoPlaces((pointsForEachCondition / racesAtGoingGroup.Count()));
                foreach (var condition4 in racesAtGoingGroup)
                {
                    var cRace = condition4.Race;
                    var placePosition = SharedCalculations.GetTake(cRace.no_of_horses ?? 0);

                    if (condition4.position == 1)
                    {
                        tracker.TotalPointsForGoing += pointsForEachC4;
                        tracker.GetHorsePreferencesDescription += $"--plus {pointsForEachC4} for winning race at Going--";
                        result += pointsForEachC4;
                    }
                    else if (condition4.position <= placePosition)
                    {
                        tracker.TotalPointsForGoing += FormatHelper.ToTwoPlaces((pointsForEachC4 / 2));
                        tracker.GetHorsePreferencesDescription += $"--plus {FormatHelper.ToTwoPlaces((pointsForEachC4 / 2))} for placing race at Going--";
                        result += FormatHelper.ToTwoPlaces((pointsForEachC4 / 2));
                    }
                }
            }

            tracker.TotalPoints += FormatHelper.ToTwoPlaces(result);
            return tracker;
        }

        private int? ConfigureRPR(RaceHorseEntity raceHorse, DateTime created)
        {
            var result = 0;

            if (raceHorse != null && raceHorse.Horse.Archive != null && raceHorse.Horse.Archive.Count() != 0)
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
