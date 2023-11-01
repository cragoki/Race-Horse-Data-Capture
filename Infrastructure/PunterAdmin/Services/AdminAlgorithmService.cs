using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Infrastructure.PunterAdmin.Helpers;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private IConfigurationRepository _configurationRepository;
        private IHorseRepository _horseRepository;
        private IMyModel _myModel;


        public AdminAlgorithmService(IAlgorithmRepository algorithmRepository, IEventRepository eventRepository, ITopSpeedOnly topSpeed, ITsRPR topSpeedRpr, IAlgorithmService algorithmService, IFormAlgorithm form, IMappingTableRepository mappingRepository, IFormRevamped formRevamp, IBentnersModel bentnersModel, IConfigurationRepository configurationRepository, IHorseRepository horseRepository, IMyModel myModel)
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
            _configurationRepository = configurationRepository;
            _horseRepository = horseRepository;
            _myModel = myModel;
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
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
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
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
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

                                var predictions = await _bentnersModel.RunModel(race);

                                if (predictions == null || predictions.Count() == 0)
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                {
                                    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                    thisRace.AlgorithmRan = true;
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
                                    horse.PredictedPosition = prediction.i + 1;
                                    horse.HorseReliability = prediction.value.Predictability;
                                    horse.Points = prediction.value.Points;
                                    horse.PointsDescription = prediction.value.PointsDescription;
                                }
                            }
                        }
                        break;
                    case (int)AlgorithmEnum.MyModel:
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

                                var predictions = await _myModel.RunModel(race);

                                if (predictions == null || predictions.Count() == 0)
                                {
                                    continue;
                                }

                                foreach (var prediction in predictions.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                                {
                                    var thisRace = even.EventRaces.Where(x => x.RaceId == race.race_id).FirstOrDefault();
                                    thisRace.AlgorithmRan = true;
                                    var horse = thisRace.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
                                    horse.PredictedPosition = prediction.i + 1;
                                    horse.HorseReliability = prediction.value.Predictability;
                                    horse.Points = prediction.value.Points;
                                    horse.PointsDescription = prediction.value.PointsDescription;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
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
                    case (int)AlgorithmEnum.MyModel:
                        foreach (var even in allEvents)
                        {
                            algorithmResult = await _myModel.GenerateAlgorithmResult(even.Races);
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
                var algorithmEntity = _algorithmRepository.GetAlgorithmById(algorithmId);
                var settings = await GetAlgorithmSettings(algorithmEntity.Settings);
                var variables = await GetAlgorithmVariables(algorithmEntity.Variables);
                var distances = _mappingRepository.GetDistanceTypes();
                var goings = _mappingRepository.GetGoingTypes();
                var predictions = new List<FormResultModel>();

                switch (algorithmEntity.algorithm_id)
                {
                    case ((int)AlgorithmEnum.FormOnly):
                        predictions = await _form.FormCalculationPredictions(race, algorithmEntity.Settings, distances, goings);
                        break;
                    case ((int)AlgorithmEnum.FormRevamp):
                        predictions = await _formRevamp.FormCalculationPredictions(race, algorithmEntity.Settings, distances, goings);
                        break;
                    case ((int)AlgorithmEnum.BentnersModel):
                        predictions = await _bentnersModel.RunModel(race);
                        break;
                    case ((int)AlgorithmEnum.MyModel):
                        predictions = await _myModel.RunModel(race);
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
                    var horse = result.Horses.Where(x => x.HorseId == prediction.value.horse_id).FirstOrDefault();
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

                result += "public static RaceEntity " + unitTestTitle + "() {" + Environment.NewLine;
                result += "return " + TestCaseHelper.NewEntity("RaceEntity");
                result += $"race_id = {raceId},{Environment.NewLine}";
                result += $"event_id = {race.event_id}, {Environment.NewLine}";
                result = EntityGenerator.GenerateEvent(result, race);
                result += $"race_time = {TestCaseHelper.FormatString(race.race_time)}, {Environment.NewLine}";
                result += $"rp_race_id = {race.rp_race_id}, {Environment.NewLine}";
                result += $"weather = {race.weather}, {Environment.NewLine}";
                //Weather Entity
                result = EntityGenerator.GenerateWeather(result, race);
                result += $"no_of_horses = {race.no_of_horses}, {Environment.NewLine}";
                result += $"going = {race.going}, {Environment.NewLine}";
                //Going Entity
                result = EntityGenerator.GenerateGoing(result, race);
                result += $"distance = {race.distance}, {Environment.NewLine}";
                //Distance Entity
                result = EntityGenerator.GenerateDistance(result, race);
                result += $"race_class = {race.race_class}, {Environment.NewLine}";
                result += $"ages = {race.ages}, {Environment.NewLine}";
                //Ages Entity
                result = EntityGenerator.GenerateAges(result, race);
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
                    result = EntityGenerator.GenerateHorse(result, race, horse);
                    result += $"weight = {TestCaseHelper.FormatString(horse.weight)},{Environment.NewLine}";
                    result += $"age = {horse.age},{Environment.NewLine}";
                    result += $"trainer_id = {horse.trainer_id},{Environment.NewLine}";
                    //TRAINER ENTITY
                    result = EntityGenerator.GenerateTrainer(result, horse);
                    result += $"jockey_id = {horse.jockey_id},{Environment.NewLine}";
                    //Jockey ENTITY
                    result = EntityGenerator.GenerateJockey(result, horse);
                    result += $"finished = {horse.finished.ToString().ToLower()},{Environment.NewLine}";
                    result += $"position = {horse.position},{Environment.NewLine}";
                    result += TestCaseHelper.CloseEntity(",");
                }
                result += "}";
                //Race End
                result += TestCaseHelper.CloseEntity(";") + "}";
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

        public List<FailedResultsViewModel> GetFailedResultsTable()
        {
            var result = new List<FailedResultsViewModel>();

            try
            {
                var failedResults = _configurationRepository.GetFailedResults();

                foreach (var failedResult in failedResults)
                {
                    var raceHorse = _horseRepository.GetRaceHorseById(failedResult.race_horse_id);

                    result.Add(new FailedResultsViewModel()
                    {
                        Id = failedResult.failed_result_id,
                        RaceHorseId = failedResult.race_horse_id,
                        Position = raceHorse.position,
                        Description = failedResult.error_message,
                        RaceUrl = $"https://www.racingpost.com/{raceHorse.Race.race_url.Replace("racecards", "results")}",
                        HorseName = raceHorse.Horse.horse_name
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public List<FailedRacesViewModel> GetFailedRacesTable()
        {
            var result = new List<FailedRacesViewModel>();

            try
            {
                var failedRaces = _configurationRepository.GetFailedRaces();

                foreach (var failedRace in failedRaces)
                {

                    result.Add(new FailedRacesViewModel()
                    {
                        Id = failedRace.failed_race_id,
                        RaceId = failedRace.race_id,
                        Description = failedRace.error_message,
                        RaceUrl = $"https://www.racingpost.com/{failedRace.Race.race_url}",
                        Attempts = failedRace.attempts
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    RaceType = race.Event.MeetingType.meeting_type,
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
