using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algorithms
{
    public class FormAlgorithm : IFormAlgorithm
    {
        private readonly IConfigurationRepository _configRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IHorseRepository _horseRepository;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public FormAlgorithm(IConfigurationRepository configRepository, IEventRepository eventRepository, IHorseRepository horseRepository)
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
                    var predictionResult = await FormCalculation(race, algorithms);

                    if (predictionResult != -1)
                    {
                        Console.WriteLine($"Valid Prediction with {predictionResult}%");
                        raceCounter++;
                        runningTotal.Add(predictionResult);
                    }
                }

                //get average of running total and store result
                result.Accuracy = (decimal)runningTotal.Average();
                result.RacesFiltered = raceCounter;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task<double> FormCalculation(RaceEntity race, List<AlgorithmVariableEntity> variables)
        {
            var settings = _configRepository.GetAlgorithmSettings((int)AlgorithmEnum.FormOnly);
            var total = SharedCalculations.GetTake(race.no_of_horses ?? 0);
            var counter = 0;
            var horses = race.RaceHorses;
            var listOfHorses = new List<HorseEntity>();
            var horseRequired = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horsesrequired.ToString()).FirstOrDefault().setting_value.ToString());
            var raceRequired = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.racesrequired.ToString())?.FirstOrDefault().setting_value.ToString());

            foreach (var h in horses)
            {
                var even = race.Event;
                var horse = h.Horse;

                if (horse.Races.Where(x => x.race_id != race.race_id).Count() >= raceRequired)
                {
                    listOfHorses.Add(horse);
                }

            }

            if (listOfHorses.Count > 0)
            {
                Dictionary<int, decimal> horseDict = new Dictionary<int, decimal>();
                var results = horses.Where(x => x.position > 0).OrderBy(x => x.position).ToList().Take(total);
                var predictions = new List<int>();
                var horseFormModel = new List<HorseFormModel>();
                var take = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horsesrequired.ToString()).FirstOrDefault().setting_value.ToString());
                //Percentage of horses required in the race with sufficient data to continue
                var variance = (int)(race.RaceHorses.Count() * take);

                //Percentage of horses required in the race with sufficient data to continue
                //TODO: Add in filter here to ensure the LAST x RACES WERE WITHIN x MONTHS
                if (listOfHorses.Count() < variance)
                {
                    return -1;
                }

                foreach (var variable in variables)
                {
                    switch (variable.variable_id)
                    {
                        case (int)VariableEnum.Form:
                            predictions = Form.CalculateResultsByForm(horseFormModel, variance).ToList();
                            break;
                    }

                    for (int i = 0; i < predictions.Count(); i++)
                    {
                        var position = i + 1;
                        //Check if horse exists in dictionary
                        if (horseDict.ContainsKey(predictions[i]))
                        {
                            //Add points based on position 1,2 or 3
                            horseDict[predictions[i]] += variable.threshold / position;
                        }
                        else
                        {
                            //Add points based on position 1,2 or 3
                            horseDict.Add(predictions[i], variable.threshold / position);
                        }
                    }
                }
                //reorder outcome based on points
                var guess = horseDict.OrderByDescending(x => x.Value);

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
