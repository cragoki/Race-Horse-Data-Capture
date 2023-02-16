using Infrastructure.PunterAdmin.ViewModels;
using Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Core.Enums;
using Core.Interfaces.Algorithms;
using System.Linq;
using Core.Entities;
using Core.Models.Algorithm;
using Core.Interfaces.Services;
using Core.Helpers;
using Core.Models;
using Org.BouncyCastle.Utilities;
using Core.Models.GetRace;

namespace Infrastructure.PunterAdmin.Services
{
    public class AdminAlgorithmService : IAdminAlgorithmService
    {
        private IAlgorithmRepository _algorithmRepository;
        private IMappingTableRepository _mappingRepository;
        private IAlgorithmService _algorithmService;
        private IEventRepository _eventRepository;
        private ITopSpeedOnly _topSpeed;
        private ITsRPR _topSpeedRpr;
        private IFormAlgorithm _form;
        private IFormRevamped _formRevamp;
        private IBentnersModel _bentnersModel;


        public AdminAlgorithmService(IAlgorithmRepository algorithmRepository, IEventRepository eventRepository, ITopSpeedOnly topSpeed, ITsRPR topSpeedRpr, IAlgorithmService algorithmService, IFormAlgorithm form, IMappingTableRepository mappingRepository, IFormRevamped formRevamp, IBentnersModel bentnersModel)
        {
            _algorithmRepository = algorithmRepository;
            _eventRepository = eventRepository;
            _topSpeed = topSpeed;
            _topSpeedRpr = topSpeedRpr;
            _algorithmService = algorithmService;
            _form = form;
            _mappingRepository = mappingRepository;
            _formRevamp = formRevamp;
            _bentnersModel = bentnersModel;
        }

        public async Task<List<AlgorithmTableViewModel>> GetAlgorithmTableData()
        {
            var result = new List<AlgorithmTableViewModel>();

            try
            {
                var algorithms = _algorithmRepository.GetAlgorithms();

                foreach (var algorithm in algorithms)
                {

                    var settings = await GetAlgorithmSettings(algorithm.Settings);
                    var variables = await GetAlgorithmVariables(algorithm.Variables);

                    var toAdd = new AlgorithmTableViewModel()
                    {
                        AlgorithmId = algorithm.algorithm_id,
                        AlgorithmName = algorithm.algorithm_name,
                        Accuracy = algorithm.accuracy ?? 0,
                        IsActive = algorithm.active,
                        NumberOfRaces = algorithm.number_of_races,
                        Settings = settings,
                        Variables = variables,
                        ShowSettings = false,
                        ShowVariables = false
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

        public async Task<List<TodaysRacesViewModel>> RunAlgorithm(AlgorithmTableViewModel algorithm, List<TodaysRacesViewModel> events)
        {
            try 
            {
                switch (algorithm.AlgorithmId) 
                {
                    case (int)AlgorithmEnum.TopSpeedOnly:
                        foreach (var even in events) 
                        {
                            var races = _eventRepository.GetRacesForEvent(even.EventId);

                            foreach (var race in even.EventRaces) 
                            {
                                race.Horses.Select(x => { x.PredictedPosition = null; return x; }).ToList();
                            }

                            foreach (var race in races)
                            {
                                var settings = await BuildAlgorithmSettings(algorithm);

                                var predictions = await _topSpeed.TopSpeedVariablePredictions(race, settings);

                                if (predictions == null || predictions.Count() == 0) 
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.Select((value, i) => new { i, value })) 
                                {
                                    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                    thisRace.AlgorithmRan = true;
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
                                    horse.PredictedPosition = prediction.i + 1;
                                    horse.Ts = prediction.value.top_speed;
                                    horse.RPR = prediction.value.rpr;
                                }
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.TsRPR:
                        foreach (var even in events)
                        {
                            var races = _eventRepository.GetRacesForEvent(even.EventId);

                            foreach (var race in even.EventRaces)
                            {
                                race.Horses.Select(x => { x.PredictedPosition = null; return x; }).ToList();
                            }

                            foreach (var race in races)
                            {
                                var settings = await BuildAlgorithmSettings(algorithm);
                                var variables = await BuildAlgorithmVariables(algorithm);
                                var predictions = await _topSpeedRpr.TSRpRCalculationPredictions(race, variables, settings);

                                if (predictions == null || predictions.Count() == 0)
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.Select((value, i) => new { i, value }))
                                {
                                    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                    thisRace.AlgorithmRan = true;
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
                                    horse.PredictedPosition = prediction.i + 1;
                                    horse.Ts = prediction.value.top_speed;
                                    horse.RPR = prediction.value.rpr;
                                }
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.FormOnly:
                        foreach (var even in events)
                        {
                            var races = _eventRepository.GetRacesForEvent(even.EventId).ToList();
                            var distances = _mappingRepository.GetDistanceTypes();
                            var goings = _mappingRepository.GetGoingTypes();

                            foreach (var race in even.EventRaces)
                            {
                                race.Horses.Select(x => { x.PredictedPosition = null; return x; }).ToList();
                            }

                            foreach (var race in races)
                            {

                                var settings = await BuildAlgorithmSettings(algorithm);

                                var predictions = await _form.FormCalculationPredictions(race, settings, distances, goings);

                                if (predictions == null || predictions.Count() == 0)
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                {
                                    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                    thisRace.AlgorithmRan = true;
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.Horse.horse_id).FirstOrDefault();
                                    horse.PredictedPosition = prediction.i + 1;
                                    horse.HorseReliability = prediction.value.Predictability;
                                    horse.Points = prediction.value.Points;
                                }
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.FormRevamp:
                        foreach (var even in events)
                        {
                            var races = _eventRepository.GetRacesForEvent(even.EventId).ToList();
                            var distances = _mappingRepository.GetDistanceTypes();
                            var goings = _mappingRepository.GetGoingTypes();

                            foreach (var race in even.EventRaces)
                            {
                                race.Horses.Select(x => { x.PredictedPosition = null; return x; }).ToList();
                            }

                            foreach (var race in races)
                            {

                                var settings = await BuildAlgorithmSettings(algorithm);

                                var predictions = await _formRevamp.FormCalculationPredictions(race, settings, distances, goings);

                                if (predictions == null || predictions.Count() == 0)
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                {
                                    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                    thisRace.AlgorithmRan = true;
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.Horse.horse_id).FirstOrDefault();
                                    horse.PredictedPosition = prediction.i + 1;
                                    horse.HorseReliability = prediction.value.Predictability;
                                    horse.Points = prediction.value.Points;
                                    horse.PointsDescription = prediction.value.PointsDescription;
                                }
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.BentnersModel:
                        foreach (var even in events)
                        {
                            var races = _eventRepository.GetRacesForEvent(even.EventId).ToList();
                            var distances = _mappingRepository.GetDistanceTypes();
                            var goings = _mappingRepository.GetGoingTypes();

                            foreach (var race in even.EventRaces)
                            {
                                race.Horses.Select(x => { x.PredictedPosition = null; return x; }).ToList();
                            }

                            foreach (var race in races)
                            {

                                var settings = await BuildAlgorithmSettings(algorithm);

                                //var predictions = await _bentnersModel.FormCalculationPredictions(race, settings, distances, goings);

                                //if (predictions == null || predictions.Count() == 0)
                                //{
                                //    continue;
                                //}

                                //foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                //{
                                //    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                //    thisRace.AlgorithmRan = true;
                                //    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.Horse.horse_id).FirstOrDefault();
                                //    horse.PredictedPosition = prediction.i + 1;
                                //    horse.HorseReliability = prediction.value.Predictability;
                                //    horse.Points = prediction.value.Points;
                                //    horse.PointsDescription = prediction.value.PointsDescription;
                                //}
                            }
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return events;
        }

        public async Task<AlgorithmTableViewModel> RunAlgorithmForAll(AlgorithmTableViewModel algorithm)
        {
            try
            {
                //var allEvents = _eventRepository.TestAlgorithmWithOneHundredEvents();
                var allEvents = _eventRepository.GetEvents();

                var results = new List<AlgorithmResult>();
                var algorithmResult = new AlgorithmResult();
                var variables = await BuildAlgorithmVariables(algorithm);
                switch (algorithm.AlgorithmId)
                {
                    case (int)AlgorithmEnum.TopSpeedOnly:
                        foreach (var even in allEvents)
                        {
                            algorithmResult = await _topSpeed.GenerateAlgorithmResult(even.Races);
                            if (algorithmResult.RacesFiltered > 0)
                            {
                                results.Add(algorithmResult);
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.TsRPR:
                        foreach (var even in allEvents)
                        {
                            algorithmResult = await _topSpeedRpr.GenerateAlgorithmResult(even.Races, variables);
                            if (algorithmResult.RacesFiltered > 0)
                            {
                                results.Add(algorithmResult);
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.FormOnly:
                        foreach (var even in allEvents)
                        {
                            algorithmResult = await _form.GenerateAlgorithmResult(even.Races, variables);
                            //If race counter > 0
                            if (algorithmResult.RacesFiltered > 0) 
                            {
                                results.Add(algorithmResult);
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.FormRevamp:
                        foreach (var even in allEvents)
                        {
                            algorithmResult = await _formRevamp.GenerateAlgorithmResult(even.Races, variables);
                            //If race counter > 0
                            if (algorithmResult.RacesFiltered > 0)
                            {
                                results.Add(algorithmResult);
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.BentnersModel:
                        foreach (var even in allEvents)
                        {
                            algorithmResult = await _bentnersModel.GenerateAlgorithmResult(even.Races);
                            //If race counter > 0
                            if (algorithmResult.RacesFiltered > 0)
                            {
                                results.Add(algorithmResult);
                            }
                        }
                        break;
                }

                if (results == null || results.Count() == 0 || results.Sum(x => x.RacesFiltered) == 0) 
                {
                    algorithm.Notes = "Failed to run";
                    return algorithm;
                }
                //Now take average of algorithmresult Accuracy and sum of races in a new algorithm result object
                var algorithmTotal = new AlgorithmResult()
                {
                    AlgorithmId = algorithm.AlgorithmId,
                    Accuracy = results.Average(x => x.Accuracy),
                    RacesFiltered = results.Sum(x => x.RacesFiltered)
                };
                //Update db
                if (algorithmTotal != null && algorithmTotal.AlgorithmId != 0)
                {
                    await _algorithmService.StoreAlgorithmResults(algorithmTotal);
                }

                algorithm.Accuracy = algorithmTotal.Accuracy;
                algorithm.NumberOfRaces = algorithmTotal.RacesFiltered;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return algorithm;
        }

        public async Task<AlgorithmTableViewModel> UpdateAlgorithmSettings(AlgorithmTableViewModel algorithm)
        {
            try
            {
                var algorithmSettings = await BuildAlgorithmSettings(algorithm);

                _algorithmRepository.UpdateAlgorithmSettings(algorithmSettings);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return algorithm;
        }

        public async Task<AlgorithmTableViewModel> UpdateAlgorithmVariables(AlgorithmTableViewModel algorithm)
        {
            try
            {
                var algorithmVariables = await BuildAlgorithmVariables(algorithm);

                _algorithmRepository.UpdateAlgorithmVariables(algorithmVariables);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return algorithm;
        }

        public async Task<RaceViewModel> GetRacePredictionsForURL(int algorithmId, string raceUrl) 
        {
            var result = new RaceViewModel();

            try
            {
                //Get Race
                raceUrl = raceUrl.Replace("https://www.racingpost.com", "");

                var race = _eventRepository.GetRaceByURL(raceUrl);
                var algorithm = new AlgorithmTableViewModel();
                if (race == null) 
                {
                    throw new Exception("No race found with selected URL");
                }

                //Switch for algorithm
                var algorithmEntity =  _algorithmRepository.GetAlgorithmById(algorithmId);
                var settings = await GetAlgorithmSettings(algorithmEntity.Settings);
                var variables = await GetAlgorithmVariables(algorithmEntity.Variables);
                var distances = _mappingRepository.GetDistanceTypes();
                var goings = _mappingRepository.GetGoingTypes();
                var predictions = new List<FormResultModel>();

                switch (algorithmEntity.algorithm_id) 
                {
                    case((int)AlgorithmEnum.FormOnly):
                            predictions = await _form.FormCalculationPredictions(race, algorithmEntity.Settings, distances, goings);
                        break;
                    case ((int)AlgorithmEnum.FormRevamp):
                            predictions = await _formRevamp.FormCalculationPredictions(race, algorithmEntity.Settings, distances, goings);
                        break;
                    default:
                        throw new Exception("Selected Algorithm is not set up for URL Predictions");
                }

                if (predictions == null || predictions.Count() == 0)
                {
                    throw new Exception("No Predictions available for selected race");
                }

                var updatedHorses = new List<RaceHorseViewModel>();
                foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                {  
                    result = BuildTodaysRaceViewModel(race, race.Event.created);
                    result.AlgorithmRan = true;
                    var horse = result.Horses.Where(x => x.HorseId == prediction.value.Horse.horse_id).FirstOrDefault();
                    horse.PredictedPosition = prediction.i + 1;
                    horse.HorseReliability = prediction.value.Predictability;
                    horse.Points = prediction.value.Points;
                    horse.PointsDescription = prediction.value.PointsDescription;
                    updatedHorses.Add(horse);
                }

                result.Horses = updatedHorses;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<string> GenerateUnitTestEntities(int raceId, string unitTestTitle)
        {
            var result = "";

            try
            {
                var race = _eventRepository.GetAllRaceDataById(raceId);

                if (race == null) 
                {
                    throw new Exception($"Could not find race with ID {raceId}");
                }

                result += "public static RaceEntity " + unitTestTitle + "() {" +  Environment.NewLine;
                result += "return " + TestCaseHelper.NewEntity("RaceEntity");
                result += $"race_id = {raceId},{Environment.NewLine}";
                result += $"event_id = {race.event_id}, {Environment.NewLine}";
                    //EventEntity
                        result += "Event = " + TestCaseHelper.NewEntity("EventEntity");
                        result += $"event_id = {race.event_id}, {Environment.NewLine}";
                        result += $"course_id = {race.Event.course_id}, {Environment.NewLine}";
                        //CourseEntity
                            result += "Course = " + TestCaseHelper.NewEntity("CourseEntity");
                            result += $"course_id = {race.Event.course_id}, {Environment.NewLine}";
                            result += $"rp_course_id = {race.Event.Course.rp_course_id ?? 0}, {Environment.NewLine}";
                            result += $"name = {TestCaseHelper.FormatString(race.Event.Course.name)}, {Environment.NewLine}";
                            result += $"country_code = {TestCaseHelper.FormatString(race.Event.Course.country_code)}, {Environment.NewLine}";
                            result += $"all_weather = {race.Event.Course.all_weather.ToString().ToLower()}, {Environment.NewLine}";
                            result += $"course_url = {TestCaseHelper.FormatString(race.Event.Course.course_url)}, {Environment.NewLine}";
                        //Course End
                        result += TestCaseHelper.CloseEntity(",");
                        result += $"abandoned = {race.Event.abandoned.ToString().ToLower()}, {Environment.NewLine}";
                        if (race.Event.surface_type != null) 
                        {
                        result += $"surface_type = {race.Event.surface_type.ToString() ?? "null"}, {Environment.NewLine}";
                            if (race.Event.surface_type != null) 
                            {
                            //SurfaceType Entity
                            result += "Surface = " + TestCaseHelper.NewEntity("SurfaceType");
                            result += $"surface_type_id = {race.Event.surface_type.ToString() ?? "null"}, {Environment.NewLine}";
                            result += $"surface_type = {TestCaseHelper.FormatString(race.Event.Surface.surface_type)}, {Environment.NewLine}";
                            //SurfaceType End
                            result += TestCaseHelper.CloseEntity(",");
                            }
                        }
                        result += $"name = {TestCaseHelper.FormatString(race.Event.name)}, {Environment.NewLine}";
                        result += $"meeting_url = {TestCaseHelper.FormatString(race.Event.meeting_url)}, {Environment.NewLine}";
                        result += $"hash_name = {TestCaseHelper.FormatString(race.Event.hash_name)}, {Environment.NewLine}";
                        result += $"meeting_type = {race.Event.meeting_type}, {Environment.NewLine}";
                        //MeetingType Entity
                            result += "MeetingType = " + TestCaseHelper.NewEntity("MeetingType");
                            result += $"meeting_type_id = {race.Event.meeting_type}, {Environment.NewLine}";
                            result += $"meeting_type = {TestCaseHelper.FormatString(race.Event.MeetingType.meeting_type)}, {Environment.NewLine}";
                            //MeetingType End
                            result += TestCaseHelper.CloseEntity(",");
                        result += $"races = {race.Event.races}, {Environment.NewLine}";
                        result += $"created = new DateTime({DateTime.Parse(race.Event.created.Date.ToString().Replace("00:00:00", ""))}), {Environment.NewLine}";
                    //Event End
                    result += TestCaseHelper.CloseEntity(",");
                result += $"race_time = {TestCaseHelper.FormatString(race.race_time)}, {Environment.NewLine}";
                result += $"rp_race_id = {race.rp_race_id}, {Environment.NewLine}";
                result += $"weather = {race.weather}, {Environment.NewLine}";
                    //Weather Entity
                    result += "Weather = " + TestCaseHelper.NewEntity("WeatherType");
                    result += $"weather_type_id = {race.Weather.weather_type_id}, {Environment.NewLine}";
                    result += $"weather_type = {TestCaseHelper.FormatString(race.Weather.weather_type)}, {Environment.NewLine}";
                    //Weather End
                    result += TestCaseHelper.CloseEntity(",");
                result += $"no_of_horses = {race.no_of_horses}, {Environment.NewLine}";
                result += $"going = {race.going}, {Environment.NewLine}";
                    //Going Entity
                    result += "Going = " + TestCaseHelper.NewEntity("GoingType");
                    result += $"going_type_id = {race.Going.going_type_id}, {Environment.NewLine}";
                    result += $"going_type = {TestCaseHelper.FormatString(race.Going.going_type)}, {Environment.NewLine}";
                    //Going End
                    result += TestCaseHelper.CloseEntity(",");
                result += $"distance = {race.distance}, {Environment.NewLine}";
                    //Distance Entity
                    result += "Distance = " + TestCaseHelper.NewEntity("DistanceType");
                    result += $"distance_type_id = {race.Distance.distance_type_id}, {Environment.NewLine}";
                    result += $"distance_type = {TestCaseHelper.FormatString(race.Distance.distance_type)}, {Environment.NewLine}";
                    //Distance End
                    result += TestCaseHelper.CloseEntity(",");
                result += $"race_class = {race.race_class}, {Environment.NewLine}";
                result += $"ages = {race.ages}, {Environment.NewLine}";
                    //Ages Entity
                    result += "Ages = " + TestCaseHelper.NewEntity("AgeType");
                    result += $"age_type_id = {race.Ages.age_type_id}, {Environment.NewLine}";
                    result += $"age_type = {TestCaseHelper.FormatString(race.Ages.age_type).Replace("\n", "").Replace("\r", "").Replace(" ", "")}, {Environment.NewLine}";
                    //Ages End
                    result += TestCaseHelper.CloseEntity(",");
                result += $"description = {TestCaseHelper.FormatString(race.description).Replace("\n", "").Replace("\r", "").Replace(" ", "")}, {Environment.NewLine}";
                result += $"race_url = {TestCaseHelper.FormatString(race.race_url)}, {Environment.NewLine}";
                result += $"completed = {race.completed.ToString().ToLower()}, {Environment.NewLine}";
                    //RACE HORSES
                    result += $"RaceHorses = new List<RaceHorseEntity>(){Environment.NewLine}";
                    result += "{";
                    result += $"{Environment.NewLine}";
                    foreach (var horse in race.RaceHorses) 
                    {
                        result += TestCaseHelper.NewEntity("RaceHorseEntity");
                            result += $"race_horse_id = {horse.race_horse_id},{Environment.NewLine}";
                            result += $"race_id = {horse.race_id},{Environment.NewLine}";
                            result += $"horse_id = {horse.horse_id},{Environment.NewLine}";
                                //HORSE ENTITY
                                result += $"Horse = {TestCaseHelper.NewEntity("HorseEntity")}";
                                    result += $"horse_id = {horse.horse_id},{Environment.NewLine}";
                                    result += $"rp_horse_id = {horse.Horse.rp_horse_id},{Environment.NewLine}";
                                    result += $"horse_name = {TestCaseHelper.FormatString(horse.Horse.horse_name)},{Environment.NewLine}";
                                    result += $"top_speed = {horse.Horse.top_speed ?? 0},{Environment.NewLine}";
                                    result += $"rpr = {horse.Horse.rpr ?? 0},{Environment.NewLine}";
                                    //GENERATE RACE HISTORY
                                    //Need a list of race horse entities, linked to a race with a set of race horses, then linked to an event
                                    result += "Races = new List<RaceHorseEntity>() {";
                                    result += $"{Environment.NewLine}";
                                    foreach(var pastRace in horse.Horse.Races.Where(x => x.Race.Event.created < race.Event.created))
                                    {
                                        //RACE HORSE ENTITY
                                            result += $"{TestCaseHelper.NewEntity("RaceHorseEntity")}";
                                            result += $"race_horse_id = {pastRace.race_horse_id},{Environment.NewLine}";
                                            result += $"horse_id = {pastRace.horse_id},{Environment.NewLine}";
                                            result += $"weight = {TestCaseHelper.FormatString(pastRace.weight)},{Environment.NewLine}";
                                            result += $"age = {pastRace.age},{Environment.NewLine}";
                                            result += $"trainer_id = {pastRace.trainer_id},{Environment.NewLine}";
                                            result += $"jockey_id = {pastRace.jockey_id},{Environment.NewLine}";
                                            result += $"finished = {pastRace.finished.ToString().ToLower()},{Environment.NewLine}";
                                            result += $"position = {pastRace.position},{Environment.NewLine}";
                                            result += $"race_id = {pastRace.race_id},{Environment.NewLine}";
                                            //GENERATE RACE ENTITY, EVENT ENTITY AND POTENTIALLY RACE HORSES <- {potentially divide the above into submethods? As this work has already been done
                                                result += $"Race = {TestCaseHelper.NewEntity("RaceEntity")}";
                                                result += $"race_id = {pastRace.race_id},{Environment.NewLine}";
                                                result += $"race_time = {TestCaseHelper.FormatString(pastRace.Race.race_time)},{Environment.NewLine}";
                                                result += $"rp_race_id = {pastRace.Race.rp_race_id},{Environment.NewLine}";
                                                result += $"weather = {pastRace.Race.weather},{Environment.NewLine}";
                                                result += $"no_of_horses = {pastRace.Race.no_of_horses},{Environment.NewLine}";
                                                result += $"going = {pastRace.Race.going.ToString() ?? "null"},{Environment.NewLine}";
                                                result += $"distance = {pastRace.Race.distance.ToString() ?? "null"},{Environment.NewLine}";
                                                result += $"race_class = {pastRace.Race.race_class.ToString() ?? "null"},{Environment.NewLine}";
                                                result += $"ages = {pastRace.Race.ages.ToString() ?? "null"},{Environment.NewLine}";
                                                result += $"completed = {pastRace.Race.completed.ToString().ToLower()},{Environment.NewLine}";
                                                result += $"event_id = {pastRace.Race.event_id},{Environment.NewLine}";
                                                    //EVENT ENTITY
                                                    result += $"Event = {TestCaseHelper.NewEntity("EventEntity")}";
                                                    result += $"event_id = {pastRace.Race.Event.event_id},{Environment.NewLine}";
                                                    result += $"course_id = {pastRace.Race.Event.course_id},{Environment.NewLine}";
                                                        //CourseEntity
                                                        result += "Course = " + TestCaseHelper.NewEntity("CourseEntity");
                                                        result += $"course_id = {pastRace.Race.Event.course_id}, {Environment.NewLine}";
                                                        result += $"rp_course_id = {pastRace.Race.Event.Course.rp_course_id ?? 0}, {Environment.NewLine}";
                                                        result += $"name = {TestCaseHelper.FormatString(pastRace.Race.Event.Course.name)}, {Environment.NewLine}";
                                                        result += $"country_code = {TestCaseHelper.FormatString(pastRace.Race.Event.Course.country_code)}, {Environment.NewLine}";
                                                        result += $"all_weather = {pastRace.Race.Event.Course.all_weather.ToString().ToLower()}, {Environment.NewLine}";
                                                        result += $"course_url = {TestCaseHelper.FormatString(pastRace.Race.Event.Course.course_url)}, {Environment.NewLine}";
                                                        //Course End
                                                        result += TestCaseHelper.CloseEntity(",");
                                                    
                                                    
                                                    result += pastRace.Race.Event.surface_type == null ? "" : $"surface_type = {pastRace.Race.Event.surface_type.ToString() ?? "null"},{Environment.NewLine}";
                                                    result += $"meeting_type = {pastRace.Race.Event.meeting_type.ToString() ?? "null"},{Environment.NewLine}";
                                                    result += $"created = new DateTime({DateTime.Parse(pastRace.Race.Event.created.Date.ToString().Replace("00:00:00", ""))}), {Environment.NewLine}";

                                                    result += TestCaseHelper.CloseEntity(",");
                                            //Event end
                                                result += TestCaseHelper.CloseEntity(",");


                                            //RACE END
                                                result += TestCaseHelper.CloseEntity(",");

                                        //RACE HORSE END

                                    }
                                    result += TestCaseHelper.CloseEntity(",");

                                    //GENERATE ARCHIVE
                                    result += "Archive = new List<HorseArchiveEntity>() {";
                                    result += $"{Environment.NewLine}";
                                    foreach (var archive in horse.Horse.Archive.Where(x => x.date <= race.Event.created)) 
                                    {
                                        //Archive entity
                                        result += $"{TestCaseHelper.NewEntity("HorseArchiveEntity")}";
                                        result += $"archive_id = {archive.archive_id},{Environment.NewLine}";
                                        result += $"horse_id = {archive.horse_id},{Environment.NewLine}";
                                        result += $"field_changed = {TestCaseHelper.FormatString(archive.field_changed)},{Environment.NewLine}";
                                        result += $"old_value = {TestCaseHelper.FormatString(archive.old_value)},{Environment.NewLine}";
                                        result += $"new_value = {TestCaseHelper.FormatString(archive.new_value)},{Environment.NewLine}";
                                        result += $"date = new DateTime({DateTime.Parse(archive.date.Date.ToString().Replace("00:00:00", ""))}), {Environment.NewLine}";
                                        result += TestCaseHelper.CloseEntity(",");
                                    }
                                //HORSE END
                                result += TestCaseHelper.CloseEntity(",");
                                result += TestCaseHelper.CloseEntity(",");

                            result += $"weight = {TestCaseHelper.FormatString(horse.weight)},{Environment.NewLine}";
                            result += $"age = {horse.age},{Environment.NewLine}";
                            result += $"trainer_id = {horse.trainer_id},{Environment.NewLine}";
                            //TRAINER ENTITY
                                result += $"Trainer = {TestCaseHelper.NewEntity("TrainerEntity")}";
                                    result += $"trainer_id = {horse.trainer_id},{Environment.NewLine}";
                                    result += $"trainer_name = {TestCaseHelper.FormatString(horse.Trainer.trainer_name)},{Environment.NewLine}";
                            //TRAINER END
                            result += TestCaseHelper.CloseEntity(",");

                            result += $"jockey_id = {horse.jockey_id},{Environment.NewLine}";
                            //Jockey ENTITY
                                    result += $"Jockey = {TestCaseHelper.NewEntity("JockeyEntity")}";
                                    result += $"jockey_id = {horse.jockey_id},{Environment.NewLine}";
                                    result += $"jockey_name = {TestCaseHelper.FormatString(horse.Jockey.jockey_name)},{Environment.NewLine}";
                            //Jockey END
                            result += TestCaseHelper.CloseEntity(",");

                            result += $"finished = {horse.finished.ToString().ToLower()},{Environment.NewLine}";
                            result += $"position = {horse.position},{Environment.NewLine}";
                    result += TestCaseHelper.CloseEntity(",");
                    }
                    result += "}";
                //Race End
                result += TestCaseHelper.CloseEntity(";") +"}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
            #region private
        private async Task<List<AlgorithSettingsTableViewModel>> GetAlgorithmSettings(List<AlgorithmSettingsEntity> settings)
        {
            var result = new List<AlgorithSettingsTableViewModel>();

            foreach (var setting in settings)
            {
                result.Add(new AlgorithSettingsTableViewModel()
                {
                    AlgorithmSettingId = setting.algorithm_setting_id,
                    SettingName = setting.setting_name,
                    SettingValue = setting.setting_value
                });
            }

            return result;
        }

        private async Task<List<AlgorithmVariableTableViewModel>> GetAlgorithmVariables(List<AlgorithmVariableEntity> variables)
        {
            var result = new List<AlgorithmVariableTableViewModel>();

            foreach (var algorithmVariable in variables)
            {
                var variable = _algorithmRepository.GetVariableById(algorithmVariable.variable_id);
                result.Add(new AlgorithmVariableTableViewModel()
                {
                    AlgorithmVariableId = algorithmVariable.algorithm_variable_id,
                    Threshold = algorithmVariable.threshold,
                    VariableName = variable.variable_name,
                    VariableId = variable.variable_id
                });
            }

            return result;
        }

        public async Task<List<AlgorithmSettingsEntity>> BuildAlgorithmSettings(AlgorithmTableViewModel algorithm) 
        {
            var result = new List<AlgorithmSettingsEntity>();

            foreach (var setting in algorithm.Settings)
            {
                result.Add(new AlgorithmSettingsEntity()
                {
                    setting_name = setting.SettingName,
                    algorithm_id = algorithm.AlgorithmId,
                    setting_value = setting.SettingValue,
                    algorithm_setting_id = setting.AlgorithmSettingId
                });
            }

            return result;
        }

        private async Task<List<AlgorithmVariableEntity>> BuildAlgorithmVariables(AlgorithmTableViewModel algorithm)
        {
            var result = new List<AlgorithmVariableEntity>();

            foreach (var variable in algorithm.Variables)
            {
                result.Add(new AlgorithmVariableEntity()
                {
                    algorithm_id = algorithm.AlgorithmId,
                    algorithm_variable_id = variable.AlgorithmVariableId,
                    threshold = variable.Threshold,
                    variable_id = variable.VariableId
                });
            }

            return result;
        }

        private RaceViewModel BuildTodaysRaceViewModel(RaceEntity race, DateTime date)
        {
            var result = new RaceViewModel();

            try
            {
                    result = new RaceViewModel()
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
                var predictedPositions = _algorithmRepository.GetAlgorithmPrediction(raceHorse.race_horse_id);
                var predictedPosition = predictedPositions.Where(x => x.algorithm_id == (int)AlgorithmEnum.FormRevamp).FirstOrDefault();

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
                    Points = racePoints
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
        #endregion
    }
}
