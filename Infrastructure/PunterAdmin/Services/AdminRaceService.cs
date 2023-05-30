
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.PunterAdmin.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public class AdminRaceService : IAdminRaceService
    {
        private static IConfigurationRepository _configRepo;
        private static IEventRepository _eventRepository;
        private static IHorseRepository _horseRepository;
        private static IAlgorithmRepository _algorithmRepository;
        private readonly DbContextData _context;

        public AdminRaceService(IConfigurationRepository configRepo, IEventRepository eventRepository, IHorseRepository horseRepository, IAlgorithmRepository algorithmRepository, DbContextData context)
        {
            _configRepo = configRepo;
            _eventRepository = eventRepository;
            _horseRepository = horseRepository;
            _algorithmRepository = algorithmRepository;
            _context = context;
        }

        public async Task<List<TodaysRacesViewModel>> GetTodaysRaces(RaceRetrievalType retrievalType, Guid? batchId)
        {
            var result = new List<TodaysRacesViewModel>();

            try
            {
                var batch = new BatchEntity();

                switch (retrievalType) 
                {
                    case RaceRetrievalType.Current:
                            batch = _configRepo.GetMostRecentBatch();
                        break;
                    case RaceRetrievalType.Next:
                            batch = _configRepo.GetNextBatch(batchId ?? new Guid());
                        break;
                    case RaceRetrievalType.Previous:
                            batch = _configRepo.GetPreviousBatch(batchId ?? new Guid());
                        break;
                }

                var checkIfFirst = _configRepo.GetPreviousBatch(batch.batch_id);
                var checkIfLast = (batch.date.Date == DateTime.Now.Date);
                //Get Events linked to that batch
                var events = _eventRepository.GetEventsByBatch(batch.batch_id);

                //foreach event in events
                foreach (var ev in events)
                {
                    var toAdd = new TodaysRacesViewModel()
                    {
                        EventId = ev.event_id,
                        EventName = ev.name,
                        Track = ev.Course?.name,
                        MeetingURL = $"https://www.racingpost.com/{ev.meeting_url}",
                        MeetingType = ev.MeetingType?.meeting_type,
                        SurfaceType = String.IsNullOrEmpty(ev.Surface?.surface_type) ? "Unknown" : ev.Surface?.surface_type,
                        EventRaces =  BuildTodaysRaceViewModel(ev.Races, ev.created), 
                        NumberOfRaces = ev.Races.Count(),
                        IsMostRecent = checkIfLast,
                        IsFirst = checkIfFirst == null ? true : false,
                        BatchId = batch.batch_id
                    };

                    result.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<RaceHorseStatisticsViewModel> GetHorseStatistics(RaceHorseViewModel raceHorse, RaceViewModel race)
        {
            var result = new RaceHorseStatisticsViewModel();

            try
            {
                result.HorseId = raceHorse.HorseId;
                result.Age = raceHorse.Age;
                result.HorseName = raceHorse.Name;
                result.RPR = raceHorse.RPR;
                result.TS = raceHorse.Ts;
                result.Weight = raceHorse.Weight;
                result.CurrentRace = race;
                result.Horse = raceHorse;
                result.HorseRaces = GetRacesForHorse(raceHorse.HorseId);
                result.Tracker = GetTrackerData(raceHorse.RaceHorseId);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<RaceViewModel> GetRacesForHorse(int horseId)
        {
            var result = new List<RaceViewModel>();
            try 
            {
                var horse = _horseRepository.GetHorseWithRaces(horseId);

                foreach (var race in horse.Races.OrderByDescending(x => x.Race.Event.created))
                {
                    var toAdd = new RaceViewModel()
                    {
                        RaceId = race.race_id,
                        Date = race.Race.Event.created,
                        Ages = race.Race.Ages?.age_type,
                        Completed = race.Race.completed,
                        Description = race.description,
                        Distance = race.Race.Distance?.distance_type,
                        EventId = race.Race.event_id,
                        Going = $"Going: {race.Race.Going?.going_type}",
                        NumberOfHorses = $"{race.Race.no_of_horses} Horses",
                        RaceClass = $"Class: {race.Race.race_class ?? 0}",
                        RaceTime = race.Race.race_time,
                        RaceUrl = $"https://www.racingpost.com/{race.Race.race_url}",
                        Stalls = $"Stalls: {race.Race.Stalls?.stalls_type}",
                        Weather = $"Weather: {race.Race.Weather?.weather_type}",
                        Horses = BuildRaceHorseViewModel(race.Race.RaceHorses, race.Race.Event)
                    };

                    if (toAdd.Horses.Any(x => x.PredictedPosition != 0)) 
                    {
                        toAdd.AlgorithmRan = true;
                    }

                    result.Add(toAdd);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public RaceHorseTrackerViewModel GetTrackerData(int race_horse_id) 
        {
            var result = new RaceHorseTrackerViewModel();

            try
            {
                var algoTracker = _algorithmRepository.GetAlgorithmTracker(race_horse_id);

                if (algoTracker != null) 
                {
                    result = new RaceHorseTrackerViewModel()
                    {
                        Id = algoTracker.algorithm_tracker_id,
                        RaceHorseId = algoTracker.race_horse_id,
                        TotalPoints = algoTracker.total_points,
                        TotalPointsForGetCurrentCondition = algoTracker.total_points_for_get_current_condition,
                        TotalPointsForLastTwoRaces = algoTracker.total_points_for_last_two_races,
                        TotalPointsForTimeSinceLastRace = algoTracker.total_points_for_time_since_last_race,
                        GetCurrentConditionDescription = algoTracker.get_current_condition_description,
                        TotalPointsForPastPerformance = algoTracker.total_points_for_past_performance,
                        GetPastPerformanceDescription = algoTracker.get_past_performance_description,
                        TotalPointsForAdjustmentsPastPerformance = algoTracker.total_points_for_adjustments_past_performance,
                        TotalPointsForStrengthOfCompetition = algoTracker.total_points_for_strength_of_competition,
                        TotalPointsForWeight = algoTracker.total_points_for_weight,
                        TotalPointsForJockeyContribution = algoTracker.total_points_for_jockey_contribution,
                        GetPastPerformanceAdjustmentsDescription = algoTracker.get_past_performance_adjustments_description,
                        PointsGivenForJockey = algoTracker.points_given_for_jockey,
                        PointsGivenForTrainer = algoTracker.points_given_for_trainer,
                        GetPresentRaceFactorsDescription = algoTracker.get_present_race_factors_description,
                        TotalPointsForSpecificTrack = algoTracker.total_points_for_specific_track,
                        TotalPointsForDistance = algoTracker.total_points_for_distance,
                        TotalPointsForRaceType = algoTracker.total_points_for_race_type,
                        TotalPointsForGoing = algoTracker.total_points_for_going,
                        GetHorsePreferencesDescription = algoTracker.get_horse_preferences_description,
                        Created = algoTracker.created
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<RaceViewModel> BuildTodaysRaceViewModel(List<RaceEntity> races, DateTime date) 
        {
            var result = new List<RaceViewModel>();

            try
            {
                foreach (var race in races)
                {

                    var toAdd = new RaceViewModel()
                    {
                        RaceId = race.race_id,
                        Date = date,
                        Ages = race.Ages?.age_type,
                        Completed = race.completed,
                        Description = race.description,
                        Distance = race.Distance?.distance_type,
                        EventId = race.event_id,
                        Going = $"Going: {race.Going?.going_type}",
                        NumberOfHorses = $"{race.no_of_horses} Horses",
                        RaceClass = $"Class: {race.race_class ?? 0}",
                        RaceTime = race.race_time,
                        RaceUrl = $"https://www.racingpost.com/{race.race_url}",
                        Stalls = $"Stalls: {race.Stalls?.stalls_type}",
                        Weather = $"Weather: {race.Weather?.weather_type}",
                        Horses = BuildRaceHorseViewModel(race.RaceHorses, race.Event)
                    };

                    result.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<RaceHorseViewModel> BuildRaceHorseViewModel(List<RaceHorseEntity> raceHorses, EventEntity even) 
        {
            var result = new List<RaceHorseViewModel>();

            foreach (var raceHorse in raceHorses)
            {
                var racePredictedPosition = 0;
                var pointsDescription = "";
                decimal racePoints = 0;
                var rpr = ConfigureRPR(raceHorse, even.created);
                var ts = ConfigureTS(raceHorse, even.created);
                //FIXFIXFIX
                var predictedPositions = _algorithmRepository.GetAlgorithmPrediction(raceHorse.race_horse_id).ToList();
                var predictedPosition = predictedPositions.Where(x => x.Algorithm.active).FirstOrDefault();

                if (predictedPosition != null) 
                {
                    racePredictedPosition = predictedPosition.predicted_position;
                    racePoints = predictedPosition.points;
                    pointsDescription = predictedPosition.points_description;
                }
                result.Add(new RaceHorseViewModel()
                {
                    RaceHorseId = raceHorse.race_horse_id,
                    Name = raceHorse.Horse.horse_name,
                    HorseId = raceHorse.horse_id,
                    Age = raceHorse.age,
                    Description = raceHorse.description,
                    Position = raceHorse.position,
                    RaceId = raceHorse.race_id,
                    Weight = raceHorse.weight,
                    JockeyName = raceHorse.Jockey?.jockey_name,
                    TrainerName = raceHorse.Trainer?.trainer_name,
                    Ts = ts,
                    RPR = rpr,
                    PredictedPosition = racePredictedPosition,
                    PointsDescription = pointsDescription,
                    Points = racePoints,
                    HorseReliability = predictedPosition?.horse_predictability
                });
            }

            return result;
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

        private int? ConfigureTS(RaceHorseEntity raceHorse, DateTime created)
        {
            var result = 0;

            if (raceHorse.Horse.Archive != null && raceHorse.Horse.Archive.Count() != 0)
            {
                var archive = raceHorse.Horse.Archive;
                var tsString = archive.Where(x => x.field_changed == "ts" && x.date < created)
                    .OrderByDescending(x => x.date).FirstOrDefault()?.new_value;

                if (tsString != "-")
                {
                    if (Int32.TryParse(tsString, out var tsInt))
                    {
                        result = tsInt;
                    }
                }
            }

            return result;
        }
    }
}
