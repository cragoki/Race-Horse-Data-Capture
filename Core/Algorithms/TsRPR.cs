using Core.Entities;
using Core.Enums;
using Core.Interfaces.Data.Repositories;
using Core.Models.Algorithm;
using Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Algorithms;

namespace Core.Algorithms
{
    public class TsRPR : ITsRPR
    {
        private readonly IConfigurationRepository _configRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IHorseRepository _horseRepository;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TsRPR(IConfigurationRepository configRepository, IEventRepository eventRepository, IHorseRepository horseRepository)
        {
            _configRepository = configRepository;
            _eventRepository = eventRepository;
            _horseRepository = horseRepository;
        }
        public async Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races, List<AlgorithmVariableEntity> algorithms)
        {
            var result = new AlgorithmResult();
            try
            {
                List<double> runningTotal = new List<double>();
                int raceCounter = 0;

                foreach (var race in races)
                {
                    var predictionResult = await TSRpRCalculation(race, algorithms);

                    if (predictionResult != 500)
                    {
                        Console.WriteLine($"Valid Prediction with {predictionResult}%");
                        raceCounter++;
                        runningTotal.Add(predictionResult);
                    }
                }

                //get average of running total and store result
                result.Accuracy = runningTotal.Average();
                result.RacesFiltered = raceCounter;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task<double> TSRpRCalculation(RaceEntity race, List<AlgorithmVariableEntity> variables)
        {
            var settings = _configRepository.GetAlgorithmSettings((int)AlgorithmEnum.TsRPR);
            var total = 3; // 3 horses placed in the prediction
            var counter = 0;
            var horses = _eventRepository.GetRaceHorsesForRace(race.race_id);
            var listOfHorses = new List<HorseEntity>();

            foreach (var h in horses)
            {
                var horse = _horseRepository.GetHorse(h.horse_id);
                listOfHorses.Add(horse);
            }
            var a = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horsesrequired.ToString()).FirstOrDefault().setting_value.ToString());
            //Percentage of horses required in the race with sufficient data to continue
            var variance = (listOfHorses.Count() * a);

            if (listOfHorses.Where(x => (x.top_speed != null || x.top_speed > 0) && (x.rpr != null || x.rpr > 0)).Count() < variance)
            {
                return 500;
            }

            if (listOfHorses.Count > 0)
            {
                Dictionary<int, decimal> horseDict = new Dictionary<int, decimal>();
                var results = horses.Where(x => x.position > 0).OrderBy(x => x.position).ToList().Take(3);
                var predictions = new List<HorseEntity>();
                foreach (var variable in variables)
                {
                    switch (variable.variable_id)
                    {
                        case (int)VariableEnum.TopSpeed:
                            predictions = TopSpeed.CalculateResultsByTopSpeed(listOfHorses).ToList();
                            break;
                        case (int)VariableEnum.RPR:
                            predictions = RPR.CalculateResultsByRPR(listOfHorses).ToList();
                            break;
                    }

                    for (int i = 0; i < predictions.Count(); i++)
                    {
                        var position = i + 1;
                        //Check if horse exists in dictionary
                        if (horseDict.ContainsKey(predictions[i].horse_id))
                        {
                            //Add points based on position 1,2 or 3
                            horseDict[predictions[i].horse_id] += variable.threshold / position;
                        }
                        else
                        {
                            //Add points based on position 1,2 or 3
                            horseDict.Add(predictions[i].horse_id, variable.threshold / position);
                        }
                    }
                }
                //reorder outcome based on points
                var guess = horseDict.OrderByDescending(x => x.Value).Take(total);

                //compare with actual results
                foreach (var prediction in guess)
                {
                    //If the predictions top 3 are in the results top 3 add a point
                    if (results.Any(x => x.horse_id == prediction.Key))
                    {
                        counter = counter + 1;
                    }
                }
            }

            return (double)counter / total * 100;
        }
    }
}
