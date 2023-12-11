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
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algorithms
{
    public class MyModel : IMyModel
    {

        private readonly IDbContextData _context;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public MyModel(IDbContextData context)
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
            var raceTracker = new AlgorithmTrackerEntity();

            //Race wide factors
            // Get a list of horses who have won at the D&G Group, after analysis, inspect winning times of those races

            if (horses.Count() == 0)
            {
                return result;
            }
            foreach (var horse in horses)
            {
                var tracker = new AlgorithmTrackerEntity();
                var toAdd = new HorsePredictionModel();
                tracker.race_horse_id = horse.race_horse_id;
                tracker.created = DateTime.Now;
                tracker.algorithm_id = (int)AlgorithmEnum.MyModel;
                toAdd.points = 0;
                toAdd.horse_id = horse.horse_id;
                toAdd.race_horse_id = horse.race_horse_id;
                //Get past races for horse
                var races = horse.Horse.Races.Where(x => x.Race.Event.created < race.Event.created);

                if (!races.Any())
                {
                    result.Add(toAdd);
                    continue;
                }
                //Current Condition
                await GenerateExperienceBonus(race, horse.Horse, tracker, horse.jockey_id);
                await ClassBonus(race, horse.Horse, tracker);
                await TimeSinceLastRaceBonus(race, horse.Horse, tracker);
                await WeightBonus(race, horse.Horse, tracker);
                await ConsistencyBonus(race, horse.Horse, tracker);
                await GenerateRaceFactors(race, horse.Horse, tracker);

                toAdd.points = tracker.total_points;

                var existingAlgorithmTracker = _context.tb_algorithm_tracker.Where(x => x.race_horse_id == horse.race_horse_id && x.total_points == tracker.total_points).FirstOrDefault();
                var algorithm = _context.tb_algorithm.Where(x => x.algorithm_name == "BentersModel").FirstOrDefault();
                if (existingAlgorithmTracker == null)
                {
                    toAdd.tracker = tracker;
                }

                result.Add(toAdd);
            }

            return result;
        }

        /// <summary>
        ///Consistency bonus -> Consecutive times placed at class, racetype and distance
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> ConsistencyBonus(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;
            int places = 0;
            var multiplier = RetrieveSetting(AlgorithmSettingEnum.consistency_bonus);

            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Take(3);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    places++;
                }
            }

            result = multiplier * places;
            tracker.points_consistency_bonus = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Class bonus -> Stepping down or stepping up class?
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> ClassBonus(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.class_bonus);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }
            //Get distinct classes raced in
            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                //placed
                if (pastRace.position <= placePosition)
                {
                    if (pastRace.Race.race_class == race.race_class)
                    {
                        result += multiplier;
                    }
                    else if (pastRace.Race.race_class > race.race_class)
                    {
                        continue;
                    }
                    else
                    {
                        result += (multiplier * 2);
                    }
                }
                //didnt place
                else
                {
                    if (pastRace.Race.race_class == race.race_class)
                    {
                        result -= multiplier;
                    }
                    else if (pastRace.Race.race_class > race.race_class)
                    {
                        result -= (multiplier * 2);
                    }
                    else
                    {
                        continue;
                    }
                }

            }

            tracker.points_class_bonus = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Time Since last race bonus
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> TimeSinceLastRaceBonus(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.time_bonus);
            var races = GetBaseRaces(horse, race);

            //Get the most similar time since last ran race. if won: +0.5, if placed +0.25, if did not place -0.25
            //would be a query like,
            //get the time since last race for this current race (within a year)

            if (races == null && !races.Any())
            {
                return tracker;
            }

            var currentRaceDate = race.Event.created;
            var firstRace = races.FirstOrDefault();

            if (firstRace == null)
            {
                return tracker;
            }
            var previousRaceDate = races.FirstOrDefault().Race.Event.created;
            var daysSinceLastRace = (currentRaceDate - previousRaceDate).TotalDays;

            var historicDaysSinceLastRace = new List<DaysSinceLastRaceModel>();
            //get a dictionary of raceIds and time since last race for all of the horses races. 
            for (int i = 0; i < races.Count(); i++)
            {
                var r1 = races.Skip(i).FirstOrDefault();
                var r2 = races.Skip(i + 1).FirstOrDefault();

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
                var closestRace = races.Where(x => x.race_id == closest.RaceId).FirstOrDefault();
                var pPos = SharedCalculations.GetTake(closestRace.Race.no_of_horses ?? 0);
                var placePosition = SharedCalculations.GetTake(closestRace.Race.no_of_horses ?? 0);

                if (closestRace.position <= placePosition)
                {
                    result += multiplier;
                }
                else
                {
                    result -= multiplier;
                }
            }

            tracker.points_time_bonus = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Weight bonus
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> WeightBonus(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.weight_bonus);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            var raceHorse = race.RaceHorses.Where(x => x.horse_id == horse.horse_id).FirstOrDefault();
            var topHalf = races.OrderBy(x => x.position).Take(races.Count() / 2);
            if (topHalf.Count() > 0)
            {
                var averageWeight = topHalf.Average(x => Decimal.Parse(x.weight.Replace(" ", "").Replace("\n", "")));
                //we can do this as a reduction. if weight diff is 0.6, multiply pointsForWeight by that value and remove it from total
                var diff = Math.Abs(averageWeight - Decimal.Parse(raceHorse.weight.Replace(" ", "").Replace("\n", "")));
                result -= (multiplier * diff);
            }

            tracker.points_weight_bonus = result;
            tracker.total_points += result;
            return tracker;
        }

        #region Experience Bonuses

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for each of the race attributes
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> GenerateExperienceBonus(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker, int jockeyId)
        {
            decimal result = 0M;

            await XpTrack(race, horse, tracker);
            await XpGoing(race, horse, tracker);
            await XpDistance(race, horse, tracker);
            await XpClass(race, horse, tracker);
            await XpDG(race, horse, tracker);
            await XpDGC(race, horse, tracker);
            await XpSurfaceType(race, horse, tracker);
            await XpJockey(race, horse, tracker, jockeyId);

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and track
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpTrack(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_track);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.Race.Event.course_id == race.Event.course_id);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_track = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and going
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpGoing(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_going);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            //Configure going groups
            var goings = _context.tb_going_type.AsEnumerable();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();
            if (goingGroups == null)
            {
                return tracker;
            }

            races = races.Where(x => goingGroups.ElementIds.Contains(x.Race.going ?? 0));

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_going = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and distance
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpDistance(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_distance);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            //Configure distance groups
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            if (distanceGroups == null)
            {
                return tracker;
            }

            races = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0));

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_distance = result;
            tracker.total_points += result;
            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and class
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpClass(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            if (race.race_class == 0)
            {
                return tracker;
            }

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_class);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.Race.race_class == race.race_class);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_class = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and distance and going
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpDG(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_dg);

            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            //Configure going and distance groups
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goings = _context.tb_going_type.AsEnumerable();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            if (distanceGroups == null || goingGroups == null)
            {
                return tracker;
            }

            races = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroups.ElementIds.Contains(x.Race.going ?? 0));

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_dg = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and distance and going and class
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpDGC(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_dgc);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            //Configure going and distance groups
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goings = _context.tb_going_type.AsEnumerable();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            if (distanceGroups == null || goingGroups == null || race.race_class == 0)
            {
                return tracker;
            }

            races = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroups.ElementIds.Contains(x.Race.going ?? 0) && x.Race.race_class == race.race_class);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_dgc = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and surface type
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpSurfaceType(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            if (race.Event.surface_type == null)
            {
                return tracker;
            }

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_surface);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.Race.Event.surface_type == race.Event.surface_type);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_surface = result;
            tracker.total_points += result;

            return tracker;
        }

        /// <summary>
        ///Experience bonus -> Amount of times placed at (x) for race type and jockey
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> XpJockey(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker, int jockeyId)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.xp_jockey);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.jockey_id == jockeyId);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);

                if (pastRace.position <= placePosition)
                {
                    result += multiplier;
                }
            }

            //Update Tracker
            tracker.points_xp_jockey = result;
            tracker.total_points += result;

            return tracker;
        }

        #endregion

        #region Race Factors

        /// <summary>
        ///Get Race Factors (same as xp but uses a multiplier adjustment based on how many races prior to this one the result was.
        /// </summary>
        /// <returns></returns>
        private async Task<AlgorithmTrackerEntity> GenerateRaceFactors(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            await RfTrack(race, horse, tracker);
            await RfGoing(race, horse, tracker);
            await RfDistance(race, horse, tracker);
            await RfClass(race, horse, tracker);
            await RfDG(race, horse, tracker);
            await RfDGC(race, horse, tracker);
            await RfSurfaceType(race, horse, tracker);

            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfTrack(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_track);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.Race.Event.course_id == race.Event.course_id);

            int index = 0;
            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);
                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }
                index++;
            }

            //Update Tracker
            tracker.points_rf_track = result;
            tracker.total_points += result;

            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfGoing(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_going);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            int index = 0;
            //Configure going groups
            var goings = _context.tb_going_type.AsEnumerable();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();
            if (goingGroups == null)
            {
                return tracker;
            }

            races = races.Where(x => goingGroups.ElementIds.Contains(x.Race.going ?? 0));

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);

                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }

                index++;
            }

            //Update Tracker
            tracker.points_rf_going = result;
            tracker.total_points += result;

            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfDistance(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_distance);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            int index = 0;

            //Configure distance groups
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            if (distanceGroups == null)
            {
                return tracker;
            }

            races = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0));

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);

                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }

                index++;
            }

            //Update Tracker
            tracker.points_rf_distance = result;
            tracker.total_points += result;

            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfClass(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;
            int index = 0;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_class);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.Race.race_class == race.race_class);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);

                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }

                index++;
            }

            //Update Tracker
            tracker.points_rf_class = result;
            tracker.total_points += result;

            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfDG(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;
            int index = 0;

            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_dg);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            //Configure going and distance groups
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goings = _context.tb_going_type.AsEnumerable();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            if (distanceGroups == null || goingGroups == null)
            {
                return tracker;
            }

            races = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroups.ElementIds.Contains(x.Race.going ?? 0));

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);

                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }

                index++;
            }

            //Update Tracker
            tracker.points_rf_dg = result;
            tracker.total_points += result;
            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfDGC(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;
            int index = 0;
            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_dgc);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            //Configure going and distance groups
            var distances = _context.tb_distance_type.AsEnumerable();
            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goings = _context.tb_going_type.AsEnumerable();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            if (distanceGroups == null || goingGroups == null || race.race_class == 0)
            {
                return tracker;
            }

            races = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroups.ElementIds.Contains(x.Race.going ?? 0) && x.Race.race_class == race.race_class);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);

                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }

                index++;
            }

            //Update Tracker
            tracker.points_rf_dgc = result;
            tracker.total_points += result;
            return tracker;
        }

        private async Task<AlgorithmTrackerEntity> RfSurfaceType(RaceEntity race, HorseEntity horse, AlgorithmTrackerEntity tracker)
        {
            decimal result = 0M;
            int index = 0;
            if (race.Event.surface_type == null)
            {
                return tracker;
            }
            var multiplier = RetrieveSetting(AlgorithmSettingEnum.rf_surface);
            var races = GetBaseRaces(horse, race);

            if (races == null && !races.Any())
            {
                return tracker;
            }

            races = races.Where(x => x.Race.Event.surface_type == race.Event.surface_type);

            foreach (var pastRace in races)
            {
                var placePosition = SharedCalculations.GetTake(pastRace.Race.no_of_horses ?? 0);
                var multiplierAdjusted = GetIndexedMultiplier(multiplier, index);

                if (pastRace.position <= placePosition)
                {
                    result += multiplierAdjusted;
                }

                index++;
            }

            //Update Tracker
            tracker.points_rf_surface = result;
            tracker.total_points += result;
            return tracker;
        }
        #endregion

        private decimal RetrieveSetting(AlgorithmSettingEnum setting)
        {
            var multiplier = _context.tb_algorithm_settings.Where(x => x.setting_name == setting.ToString()).First().setting_value.ToString();

            return Decimal.Parse(multiplier);
        }

        /// <summary>
        /// Races for the past 6 months, where the race type is the same and the horse position is not 0
        /// </summary>
        /// <param name="horse"></param>
        /// <param name="race"></param>
        /// <returns></returns>
        private IQueryable<RaceHorseEntity> GetBaseRaces(HorseEntity horse, RaceEntity race)
        {
            return horse.Races.Where(x => x.position != 0 && x.Race.Event.created < race.Event.created && x.race_id != race.race_id && x.Race.Event.meeting_type == race.Event.meeting_type && x.Race.Event.created > DateTime.Now.AddMonths(-6)).OrderByDescending(x => x.Race.Event.created).AsQueryable();

        }

        private decimal GetIndexedMultiplier(decimal points, int index)
        {
            var result = points;

            if (index > 0)
            {
                switch (index)
                {
                    case 1:
                        result = points * 0.90m;
                        break;
                    case 2:
                        result = points * 0.75m;
                        break;
                    case 3:
                        result = points * 0.5m;
                        break;
                    default:
                        result = points * 0.25m;
                        break;
                }
            }

            return result;
        }
    }
}
