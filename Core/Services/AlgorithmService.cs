using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Models.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static IEventRepository _eventRepository;
        private static IAlgorithmRepository _algorithmRepository;
        private static ITopSpeedOnly _topSpeedOnly;
        private static ITsRPR _topSpeedRpr;
        private static IFormAlgorithm _formAlgorithm;
        private static IFormRevamped _formRevampedAlgorithm;
        private static IBentnersModel _bentnersAlgorithm;
        private static IMyModel _myAlgorithm;

        public AlgorithmService(IEventRepository eventRepository, IAlgorithmRepository algorithmRepository, ITopSpeedOnly topSpeedOnly, ITsRPR topSpeedRpr, IFormRevamped formRevampedAlgorithm, IFormAlgorithm formAlgorithm, IBentnersModel bentnersAlgorithm, IMyModel myAlgorithm)
        {
            _eventRepository = eventRepository;
            _algorithmRepository = algorithmRepository;
            _topSpeedOnly = topSpeedOnly;
            _topSpeedRpr = topSpeedRpr;
            _formAlgorithm = formAlgorithm;
            _formRevampedAlgorithm = formRevampedAlgorithm;
            _bentnersAlgorithm = bentnersAlgorithm;
            _myAlgorithm = myAlgorithm;
        }

        public async Task<AlgorithmResult> ExecuteActiveAlgorithm()
        {
            var result = new AlgorithmResult();
            try
            {

                var activeAlgorithm = _algorithmRepository.GetActiveAlgorithm();

                if (activeAlgorithm != null)
                {
                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                    var algorithmVariables = _algorithmRepository.GetAlgorithmVariableByAlgorithmId(activeAlgorithm.algorithm_id);
                    var races = _eventRepository.GetAllRaces();

                    switch ((AlgorithmEnum)activeAlgorithm.algorithm_id)
                    {
                        case AlgorithmEnum.TopSpeedOnly:
                            result = await _topSpeedOnly.GenerateAlgorithmResult(races);
                            break;
                        case AlgorithmEnum.TsRPR:
                            result = await _topSpeedRpr.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.FormOnly:
                            result = await _formAlgorithm.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.FormRevamp:
                            result = await _formRevampedAlgorithm.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.BentnersModel:
                            result = await _bentnersAlgorithm.GenerateAlgorithmResult(races);
                            break;
                        case AlgorithmEnum.MyModel:
                            result = await _myAlgorithm.GenerateAlgorithmResult(races);
                            break;
                    }

                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service ExecuteActiveAlgorithm...{ex.Message}");
            }

            return result;
        }

        public AlgorithmEntity GetActiveAlgorithm()
        {
            var result = new AlgorithmEntity();

            try
            {
                result = _algorithmRepository.GetActiveAlgorithm();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service GetActiveAlgorithm...{ex.Message}");
            }

            return result;
        }


        public async Task StoreAlgorithmResults(AlgorithmResult result)
        {
            try
            {
                var update = _algorithmRepository.GetAlgorithmById(result.AlgorithmId);

                update.accuracy = (decimal)result.Accuracy;
                update.number_of_races = result.RacesFiltered;
                update.active = false;

                await _algorithmRepository.UpdateActiveAlgorithm(update);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error trying to store Algorithm result...{ex.Message}");
            }
        }

        public async Task<AlgorithmResult> ExecuteSelectedAlgorithm(List<RaceEntity> algorithm, List<AlgorithmEntity> events, List<AlgorithmVariableEntity> algorithmVariables)
        {
            var result = new AlgorithmResult();
            try
            {

                var activeAlgorithm = _algorithmRepository.GetActiveAlgorithm();

                if (activeAlgorithm != null)
                {
                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                    var races = _eventRepository.GetAllRaces();

                    switch ((AlgorithmEnum)activeAlgorithm.algorithm_id)
                    {
                        case AlgorithmEnum.TopSpeedOnly:
                            result = await _topSpeedOnly.GenerateAlgorithmResult(races);
                            break;
                        case AlgorithmEnum.TsRPR:
                            result = await _topSpeedRpr.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.FormOnly:
                            result = await _formAlgorithm.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                    }

                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task<List<AlgorithmSettingsEntity>> GetSettingsForAlgorithm(int algorithm_id)
        {
            var result = new List<AlgorithmSettingsEntity>();

            try
            {
                var algorithm = _algorithmRepository.GetAlgorithms().Where(x => x.algorithm_id == algorithm_id).FirstOrDefault();

                result = algorithm.Settings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedSettingsForAlgorithm()
        {
            try
            {
                return _algorithmRepository.GetArchivedAlgorithmSettings();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SequenceCourseAccuracyEntity>> GetSequenceCourseAccuracy(Guid batch_id)
        {
            try
            {
                var result = await _algorithmRepository.GetCourseAccuracy(batch_id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ArchiveAlgorithmSettings(List<AlgorithmSettingsEntity> settings, Guid batchId)
        {
            var toAdd = new List<AlgorithmSettingsArchiveEntity>();

            foreach (var setting in settings)
            {
                toAdd.Add(new AlgorithmSettingsArchiveEntity()
                {
                    algorithm_setting_id = setting.algorithm_setting_id,
                    batch_id = batchId,
                    algorithm_id = setting.algorithm_id,
                    setting_name = setting.setting_name,
                    setting_value = setting.setting_value
                });
            }

            await _algorithmRepository.ArchiveAlgorithmSettings(toAdd);
        }

        public async Task UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> settings)
        {
            try
            {
                await _algorithmRepository.UpdateAlgorithmSettings(settings);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error trying to update algorithm settings...{ex.Message}");
            }
        }

        public async Task AddAlgorithmPrediction(AlgorithmPredictionEntity prediction)
        {
            await _algorithmRepository.AddAlgorithmPrediction(prediction);
        }

        public async Task AddAlgorithmVariableSequence(AlgorithmVariableSequenceEntity sequence)
        {
            await _algorithmRepository.AddAlgorithmVariableSequence(sequence);
        }

        public async Task AddAlgorithmTracker(AlgorithmTrackerEntity tracker)
        {
            await _algorithmRepository.AddAlgorithmTracker(tracker);
        }

        public VariableAnalysisModel IdentifyWinningAlgorithmVariables(List<AlgorithmPredictionEntity> predictions, List<AlgorithmTrackerEntity> trackers, RaceEntity race)
        {
            var result = new VariableAnalysisModel();
            result.CourseId = race.Event.course_id;
            result.IsComplete = false;
            try
            {
                //Check if placed.
                var placedPositions = SharedCalculations.GetTake(race.no_of_horses ?? 0);
                var horsesWithTrackers = trackers.Where(x => x != null).Select(x => x.race_horse_id);
                var placedHorses = race.RaceHorses.Where(x => x.position != 0 && horsesWithTrackers.Contains(x.race_horse_id)).OrderBy(x => x.position).Take(placedPositions);

                if (placedHorses == null || placedHorses.Count() == 0) 
                {
                    return result;
                }
                var predictedPlacer = predictions.FirstOrDefault(x => (x.predicted_position == 1 && race.RaceHorses.FirstOrDefault(y => y.race_horse_id == x.race_horse_id).position != 0) || (x.predicted_position == 2 && race.RaceHorses.FirstOrDefault(y => y.race_horse_id == x.race_horse_id).position != 0) || (x.predicted_position == 3 && race.RaceHorses.FirstOrDefault(y => y.race_horse_id == x.race_horse_id).position != 0));

                if (predictedPlacer == null) 
                {
                    return result;
                }

                var tracker = trackers.FirstOrDefault(x => x.race_horse_id == predictedPlacer.race_horse_id);
                if (placedHorses.Select(x => x.race_horse_id).Contains(predictedPlacer.race_horse_id))
                {
                    //Placed
                    result.IsCorrect = true;
                    result.Rankings = DetermineVariableRankings(tracker);
                    result.RaceType = race.Event.meeting_type;
                }
                else
                {
                    //Not Placed
                    result.IsCorrect = false;
                    result.RaceType = race.Event.meeting_type;
                    var accumulatedTracker = new AlgorithmTrackerEntity();

                    foreach (var placedHorse in placedHorses.Where(x => x.Horse.Races.Count() >= 2))
                    {
                        var placedHorseTracker = trackers.Where(x => x.race_horse_id == placedHorse.race_horse_id).FirstOrDefault();
                        if (placedHorseTracker == null) 
                        {
                            continue;
                        }
                        accumulatedTracker.points_xp_track += placedHorseTracker.points_xp_track;
                        accumulatedTracker.points_xp_going += placedHorseTracker.points_xp_going;
                        accumulatedTracker.points_xp_distance += placedHorseTracker.points_xp_distance;
                        accumulatedTracker.points_xp_class += placedHorseTracker.points_xp_class;
                        accumulatedTracker.points_xp_dg += placedHorseTracker.points_xp_dg;
                        accumulatedTracker.points_xp_dgc += placedHorseTracker.points_xp_dgc;
                        accumulatedTracker.points_xp_surface += placedHorseTracker.points_xp_surface;
                        accumulatedTracker.points_xp_jockey += placedHorseTracker.points_xp_jockey;
                        accumulatedTracker.points_consistency_bonus += placedHorseTracker.points_consistency_bonus;
                        accumulatedTracker.points_class_bonus += placedHorseTracker.points_class_bonus;
                        accumulatedTracker.points_time_bonus += placedHorseTracker.points_time_bonus;
                        accumulatedTracker.points_weight_bonus += placedHorseTracker.points_weight_bonus;
                        accumulatedTracker.points_rf_track += placedHorseTracker.points_rf_track;
                        accumulatedTracker.points_rf_going += placedHorseTracker.points_rf_going;
                        accumulatedTracker.points_rf_distance += placedHorseTracker.points_rf_distance;
                        accumulatedTracker.points_rf_class += placedHorseTracker.points_rf_class;
                        accumulatedTracker.points_rf_dg += placedHorseTracker.points_rf_dg;
                        accumulatedTracker.points_rf_dgc += placedHorseTracker.points_rf_dgc;
                        accumulatedTracker.points_rf_surface += placedHorseTracker.points_rf_surface;
                    }

                    result.Rankings = DetermineVariableRankings(accumulatedTracker);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            result.IsComplete = true;
            return result;
        }


        public async Task<bool> AlgorithmSettingsAreUnique(List<AlgorithmSettingsEntity> settings) 
        {
            var result = true;
            var archives = await GetArchivedSettingsForAlgorithm();

            foreach(var archive in archives.GroupBy(x => x.batch_id))
            {
                bool isIdentical = true;
                foreach (var setting in settings) 
                {
                    if (archive.FirstOrDefault(x => x.setting_name == setting.setting_name).setting_value != setting.setting_value) 
                    {
                        isIdentical = false;
                        continue;
                    }
                }
                if (isIdentical) 
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task<List<SequenceAnalysisEntity>> GetSequenceAnalysis() 
        {
            try 
            {
                return _algorithmRepository.GetSequenceAnalysis();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AlgorithmVariableSequenceEntity>> GetAlgorithmVariableSequence()
        {
            try
            {
                return _algorithmRepository.GetAlgorithmVariableSequence();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AlgorithmSettingsArchiveEntity>> GetArchivedSettingsForBatch(Guid batch_id)
        {
            try
            {
                var result = await _algorithmRepository.GetArchivedAlgorithmSettingsForBatch(batch_id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddSequenceAnalysis(SequenceAnalysisEntity entity)
        {
            try
            {
                await _algorithmRepository.AddSequenceAnalysis(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateSequenceAnalysis(SequenceAnalysisEntity entity)
        {
            try
            {
                await _algorithmRepository.UpdateSequenceAnalysis(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddCourseAccuracy(List<SequenceCourseAccuracyEntity> entites)
        {
            try
            {
                await _algorithmRepository.AddCourseAccuracy(entites);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<string> MyModelProperies = new List<string>()
        {
        "points_xp_track",
        "points_xp_going",
        "points_xp_distance",
        "points_xp_class",
        "points_xp_dg",
        "points_xp_dgc",
        "points_xp_surface",
        "points_xp_jockey",
        "points_consistency_bonus",
        "points_class_bonus",
        "points_time_bonus",
        "points_weight_bonus",
        "points_rf_track",
        "points_rf_going",
        "points_rf_distance",
        "points_rf_class",
        "points_rf_dg",
        "points_rf_dgc",
        "points_rf_surface"
        };

        private class PropertyValuePair
        {
            public string PropertyName { get; set; }
            public decimal? PropertyValue { get; set; }
        }

        private List<VariableRankings> DetermineVariableRankings(AlgorithmTrackerEntity tracker)
        {
            var rankings = new List<VariableRankings>();
            var result = new List<PropertyValuePair>();
            //19 Variables
            //So winner = 18 points, all the way down to loser which gets 0 points
            var points = 18;
            var properties = tracker.GetType().GetProperties().Where(x => MyModelProperies.Contains(x.Name));

            foreach (var property in properties)
            {
                decimal? value = property.GetValue(tracker) as decimal?;
                result.Add(new PropertyValuePair
                {
                    PropertyName = property.Name,
                    PropertyValue = value
                });
            }

            foreach (var rank in result.OrderByDescending(x => x.PropertyValue))
            {
                rankings.Add(new VariableRankings()
                {
                    AlgorithmVariable = Enum.Parse<AlgorithmSettingEnum>(rank.PropertyName.Replace("points_", "")),
                    Points = points
                });

                points--;
            }

            return rankings;
        }
    }
}
