using Core.Entities;
using Core.Enums;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Services;
using Core.Models.MachineLearning;
using Infrastructure.PunterAdmin.ViewModels;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RHDCMachineLearning
{
    public class RHDCMachineLearning : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IEventService _eventService;
        private readonly IAlgorithmService _algorithmService;
        private readonly IRaceService _raceService;
        private IMyModel _myModel;
        private readonly IConfigurationService _configService;

        public RHDCMachineLearning(IHostApplicationLifetime hostApplicationLifetime, IEventService eventService, IRaceService raceService, IAlgorithmService algorithmService, IMyModel myModel, IConfigurationService configService)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _eventService = eventService;
            _raceService = raceService;
            _algorithmService = algorithmService;
            _myModel = myModel;
            _configService = configService;
        }
        private void OnStopping()
        {
            Logger.Info("OnStopping method finished.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var job = await _configService.GetJobInfo(JobEnum.rhdcalgorithmadjuster);

            if (job.next_execution < DateTime.Now)
            {
                Console.WriteLine("Initializing Machine Learning");
                bool isFirst = true;
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {

                        Console.WriteLine($"Beginning Batch at {DateTime.Now}");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-----------------------------------Machine Learning Inilializing---------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        var batch = new Guid();
                        var result = new AlgorithmVariableSequenceEntity();
                        int numberOfRaces = 0;
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
                                    if (prediction.Average(x => x.Points) < 1)
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
                                        numberOfRaces++;
                                    }
                                }

                            }

                            //Determine the % correct and add to DB
                            decimal percentageCorrect = (rankings.Where(x => x.IsCorrect).Count() / rankings.Count()) * 100;
                            result.percentage_correct = percentageCorrect;
                            result.no_of_races = numberOfRaces;
                            result.batch_id = batch;
                            result.is_first_in_sequence = isFirst;
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
                            _algorithmService.ArchiveAlgorithmSettings(algorithmSettings, batch);

                            //Make changes to settings
                            foreach (var y in topTier)
                            {
                                var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                                setting.setting_value = (Decimal.Parse(setting.setting_value) + 0.25M).ToString();
                            }
                            foreach (var y in secondTier)
                            {
                                var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                                setting.setting_value = (Decimal.Parse(setting.setting_value) + 0.10M).ToString();
                            }
                            foreach (var y in lowerTier)
                            {
                                var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                                setting.setting_value = (Decimal.Parse(setting.setting_value) - 0.10M).ToString();
                            }
                            foreach (var y in bottomTier)
                            {
                                var setting = algorithmSettings.FirstOrDefault(x => x.setting_name == y.ToString());
                                setting.setting_value = (Decimal.Parse(setting.setting_value) - 0.25M).ToString();
                            }

                            await _algorithmService.UpdateAlgorithmSettings(algorithmSettings);
                            _algorithmService.AddAlgorithmVariableSequence(result);


                            //Save Result
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error Occured while running predictions. Error: {ex.Message} ---- Inner Exception: {ex.InnerException}");
                        }


                        Console.WriteLine($"Completed Algorithm Checks");

                        //Store Batch in the database
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-----------------------------------Machine Learning Terminating----------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"completing Batch at {DateTime.Now}");


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error! {ex.Message} Inner Exception: {ex.InnerException}");
                        Thread.Sleep((int)TimeSpan.FromMinutes(10).TotalMilliseconds);
                    }
                    isFirst = false;
                }

                await _configService.UpdateJob(JobEnum.rhdcalgorithmadjuster);
            }
            else 
            {
                Console.WriteLine($"Health check, everything Okay! The time is {DateTime.Now} Sleeping....");
            }
            Thread.Sleep((int)TimeSpan.FromMinutes(job.interval_check_minutes).TotalMilliseconds);
        }
    }
}
