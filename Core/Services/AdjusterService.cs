using Core.Entities;
using Core.Enums;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Services;
using Core.Models.MachineLearning;
using Infrastructure.PunterAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AdjusterService : IAdjusterService
    {
        private readonly IEventService _eventService;
        private readonly IAlgorithmService _algorithmService;
        private IMyModel _myModel;
        private static Guid _batch;

        public AdjusterService(IEventService eventService, IAlgorithmService algorithmService, IMyModel myModel)
        {
            _eventService = eventService;
            _algorithmService = algorithmService;
            _myModel = myModel;
        }

        public async Task AdjustAlgorithmSettings()
        {
            _batch = Guid.NewGuid();
            var result = new AlgorithmVariableSequenceEntity();
            //Get todays Events
            var events = await _eventService.GetRandomEventsFromDatabase();

            //Trigger Algorithm for todays results.
            Console.WriteLine($"Running Predictions");

            try
            {
                var rankings = new List<VariableAnalysisModel>();
                foreach (var even in events)
                {
                    var prediction = new List<FormResultModel>();
                    Console.WriteLine($"Getting Event Races at {DateTime.Now}");
                    //Get Predictions
                    var races = await _eventService.GetRacesFromDatabaseForAlgorithm(even.event_id);
                    var raceList = races.ToList();
                    foreach (var race in raceList)
                    {
                        var predictions = new List<AlgorithmPredictionEntity>();
                        var trackers = new List<AlgorithmTrackerEntity>();

                        Console.WriteLine($"Running Algorithm at {DateTime.Now}");

                        prediction = await _myModel.RunModel(race);

                        if (prediction == null || prediction.Count() == 0)
                        {
                            continue;
                        }

                        //If we have data for 40% of the horses in the race
                        decimal? fortyPercent = race.no_of_horses * 0.4M;
                        if (prediction.Count(x => x.Points != 0) >= Decimal.Round(fortyPercent.Value))
                        {
                            continue;
                        }


                        foreach (var p in prediction.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                        {
                            var algorithmPrediction = new AlgorithmPredictionEntity()
                            {
                                race_horse_id = p.value.RaceHorseId,
                                algorithm_id = (int)AlgorithmEnum.MyModel,
                                predicted_position = p.i + 1,
                                points = p.value.Points ?? 0,
                                points_description = p.value.PointsDescription,
                                horse_predictability = p.value.Predictability
                            };


                            if (p.value.Tracker != null)
                            {
                                predictions.Add(algorithmPrediction);
                                trackers.Add(p.value.Tracker);
                            }
                        }
                        //Check Predictions results
                        var ranking = _algorithmService.IdentifyWinningAlgorithmVariables(predictions, trackers, race);

                        if (ranking.IsComplete)
                        {
                            rankings.Add(ranking);
                        }
                    }

                }

                //Determine the % correct and add to DB
                var percentageCorrect = (decimal)((double)rankings.Count(x => x.IsCorrect) / rankings.Count()) * 100;
                Console.WriteLine($"Percentage Correct = {percentageCorrect}");
                result.percentage_correct = percentageCorrect;
                result.no_of_races = rankings.Count();
                result.batch_id = _batch;
                result.is_first_in_sequence = true;
                result.date = DateTime.Now;


                //Adjust Algorithm Settings
                List<VariableRankings> aggregatedRankings = rankings
                .SelectMany(model => model.Rankings)
                .GroupBy(r => r.AlgorithmVariable)
                .Select(group => new VariableRankings
                {
                    AlgorithmVariable = group.Key,
                    Points = group.Sum(r => r.Points)
                })
                .OrderByDescending(x => x.Points)
                .ToList();

                var topTier = aggregatedRankings.Take(3).Select(x => x.AlgorithmVariable); //Plus 0.25
                var secondTier = aggregatedRankings.Skip(3).Take(4).Select(x => x.AlgorithmVariable);//Plus 0.1
                var noChange = aggregatedRankings.Skip(7).Take(5).Select(x => x.AlgorithmVariable);//No Change
                var lowerTier = aggregatedRankings.Skip(12).Take(5).Select(x => x.AlgorithmVariable);//Minus 0.1
                var bottomTier = aggregatedRankings.Skip(17).Take(2).Select(x => x.AlgorithmVariable); //Minus 0.25

                var algorithmSettings = await _algorithmService.GetSettingsForAlgorithm((int)AlgorithmEnum.MyModel);

                //Archive existing settings
                await _algorithmService.ArchiveAlgorithmSettings(algorithmSettings, _batch);

                //Get all settings archive to ensure that the new settings we are about to generate, do not already exist, if they do, adjust everything by .1 and try again
                bool originalAlgorithm = false;

                while (!originalAlgorithm)
                {
                    //while settings contains our settings
                    foreach (var y in topTier)
                    {
                        var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                        setting.setting_value = (Decimal.Parse(setting.setting_value) + 0.1M).ToString();
                    }
                    foreach (var y in secondTier)
                    {
                        var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                        setting.setting_value = (Decimal.Parse(setting.setting_value) + 0.05M).ToString();
                    }
                    foreach (var y in lowerTier)
                    {
                        var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                        setting.setting_value = (Decimal.Parse(setting.setting_value) - 0.05M).ToString();
                    }
                    foreach (var y in bottomTier)
                    {
                        var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                        setting.setting_value = (Decimal.Parse(setting.setting_value) - 0.1M).ToString();
                    }

                    if (await _algorithmService.AlgorithmSettingsAreUnique(algorithmSettings))
                    {
                        originalAlgorithm = true;
                    }
                }

                await _algorithmService.UpdateAlgorithmSettings(algorithmSettings);
                await _algorithmService.AddAlgorithmVariableSequence(result);


                //Save Result
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured while adjusting settings. Error: {ex.Message} ---- Inner Exception: {ex.InnerException}");
            }
        }


        public async Task AnalyseAlgorithmSettings() 
        {
            try 
            {
                var rankings = new List<VariableAnalysisModel>();
                bool isCourseAnalysis = false;
                //Get Sequences from sequence table where the batch_id does not exist in analysis table
                var sequences = await _algorithmService.GetSequenceAnalysis();
                var existingBatches = sequences.Where(x => x.is_complete && x.last_checked <= DateTime.Now.AddDays(15)).Select(x => x.batch_id);
                var batches = await _algorithmService.GetAlgorithmVariableSequence();
                var batch = batches.OrderByDescending(x => x.percentage_correct).FirstOrDefault(x => !existingBatches.Contains(x.batch_id));

                //Get the algorithm settings from the top one batch ordered by percentage_correct desc
                var settings = await _algorithmService.GetArchivedSettingsForBatch(batch.batch_id);

                //Update the existing algorithm settings to those of the sequence
                var algorithmSettings = await _algorithmService.GetSettingsForAlgorithm((int)AlgorithmEnum.MyModel);
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_track.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_track.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_going.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_going.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_distance.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_distance.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_class.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_class.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_dg.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_dg.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_dgc.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_dgc.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_surface.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_surface.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_jockey.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.xp_jockey.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.consistency_bonus.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.consistency_bonus.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.class_bonus.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.class_bonus.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.time_bonus.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.time_bonus.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.weight_bonus.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.weight_bonus.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_track.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_track.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_going.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_going.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_distance.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_distance.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_class.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_class.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_dg.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_dg.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_dgc.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_dgc.ToString()).setting_value;
                algorithmSettings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_surface.ToString()).setting_value = settings.FirstOrDefault(x => x.setting_name == AlgorithmSettingEnum.rf_surface.ToString()).setting_value;

                await _algorithmService.UpdateAlgorithmSettings(algorithmSettings);

                //Get all event data within the past 2 months (test this locally with just .take 1 on the repo method)
                var events = await _eventService.GetLastTwoMonthsEvents();
                var sequence = new SequenceAnalysisEntity();
                if (sequences.Where(x => x.batch_id == batch.batch_id).Count() > 0)
                {
                    //In this case we would be running the course analysis and then setting the sequence to iscomplete = true after finishing
                    sequence = sequences.Where(x => x.batch_id == batch.batch_id).FirstOrDefault();
                    isCourseAnalysis = true;
                }
                else
                {
                    sequence.batch_id = batch.batch_id;
                    sequence.last_checked = DateTime.Now;
                }

                if (isCourseAnalysis) 
                {
                    //If we are doing course analysis, run predictions against only the courses with a greater than 50% accuracy
                    var courseAccuracyBatch = await _algorithmService.GetSequenceCourseAccuracy(sequence.batch_id);
                    var coursesToUse = courseAccuracyBatch.Where(x => x.percentage_correct >= 60).Select(x => x.course_id);
                    events = events.Where(x => coursesToUse.Contains(x.course_id)).ToList();
                }

                foreach (var e in events) 
                {
                    //If all racehorses have position 0, then the event is abandoned, so we can continue
                    if (e.Races.All(race => race.RaceHorses.All(horse => horse.position == 0)))
                    {
                        continue;
                    }
                    foreach (var race in e.Races) 
                    {
                        var prediction = new List<FormResultModel>();

                        if (race.RaceHorses.All(x => x.position == 0)) 
                        {
                            continue;
                        }
                        var predictions = new List<AlgorithmPredictionEntity>();
                        var trackers = new List<AlgorithmTrackerEntity>();

                        Console.WriteLine($"Running Algorithm at {DateTime.Now}");

                        prediction = await _myModel.RunModel(race);

                        if (prediction == null || prediction.Count() == 0)
                        {
                            continue;
                        }
                        //If we have data for 40% of the horses in the race
                        decimal? fortyPercent = race.no_of_horses * 0.4M;
                        if (prediction.Count(x => x.Points != 0) >= Decimal.Round(fortyPercent.Value))
                        {
                            continue;
                        }


                        foreach (var p in prediction.OrderByDescending(x => x.Points).Select((value, i) => new { i, value }))
                        {
                            var algorithmPrediction = new AlgorithmPredictionEntity()
                            {
                                race_horse_id = p.value.RaceHorseId,
                                algorithm_id = (int)AlgorithmEnum.MyModel,
                                predicted_position = p.i + 1,
                                points = p.value.Points ?? 0,
                                points_description = p.value.PointsDescription,
                                horse_predictability = p.value.Predictability
                            };


                            if (p.value.Tracker != null)
                            {
                                predictions.Add(algorithmPrediction);
                                trackers.Add(p.value.Tracker);
                            }
                        }
                        //Check Predictions results
                        var ranking = _algorithmService.IdentifyWinningAlgorithmVariables(predictions, trackers, race);

                        if (ranking.IsComplete)
                        {
                            rankings.Add(ranking);
                        }

                    }
                }

                var percentageCorrect = (decimal)((double)rankings.Count(x => x.IsCorrect) / rankings.Count()) * 100;
                Console.WriteLine($"Percentage Correct = {percentageCorrect}");

                //Add the course accuracy entity to the list
                if (isCourseAnalysis)
                {
                    sequence.percentage_correct_with_course_adjustment = percentageCorrect;
                    sequence.is_complete = true;

                    //Update Sequence
                    await _algorithmService.UpdateSequenceAnalysis(sequence);
                }
                else 
                {
                    var courseAnalysis = new List<SequenceCourseAccuracyEntity>();
                    var coursesGrouped = rankings.GroupBy(x => x.CourseId);

                    foreach (var group in coursesGrouped) 
                    {
                        courseAnalysis.Add(new SequenceCourseAccuracyEntity()
                        {
                            course_id = group.Key,
                            number_of_races = group.Count(),
                            percentage_correct = (decimal)((double)group.Count(x => x.IsCorrect) / group.Count()) * 100,
                            batch_id = sequence.batch_id
                        });
                    }

                    sequence.percentage_correct = percentageCorrect;
                    sequence.is_complete = false;

                    //Add Sequence and batches
                    await _algorithmService.AddSequenceAnalysis(sequence);
                    await _algorithmService.AddCourseAccuracy(courseAnalysis);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured while analysing settings. Error: {ex.Message} ---- Inner Exception: {ex.InnerException}");
            }
        }
    }
}
