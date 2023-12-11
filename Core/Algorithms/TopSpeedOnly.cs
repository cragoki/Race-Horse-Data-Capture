
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
    public class TopSpeedOnly : ITopSpeedOnly
    {
        private readonly IConfigurationRepository _configRepository;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IRaceService _raceService;

        public TopSpeedOnly(IConfigurationRepository configRepository, IRaceService raceService)
        {
            _configRepository = configRepository;
            _raceService = raceService;
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
                    var predictionResult = await TopSpeedVariable(race);

                    if (predictionResult != -1)
                    {
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

        public async Task<double> TopSpeedVariable(RaceEntity race)
        {
            var settings = _configRepository.GetAlgorithmSettings((int)AlgorithmEnum.TopSpeedOnly);
            var total = SharedCalculations.GetTake(race.no_of_horses ?? 0);
            var counter = 0;
            var listOfHorses = new List<HorseEntity>();

            foreach (var h in race.RaceHorses)
            {
                var even = race.Event;
                var horse = h.Horse;

                //GETTING ARCHIVED HORSE DATA
                var ts = await _raceService.GetTsForHorseRace(h.Horse.Archive, even.created);

                if (ts != -1)
                {
                    horse.top_speed = ts;
                }

                listOfHorses.Add(horse);
            }

            if (listOfHorses.Count > 0)
            {
                var a = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horsesrequired.ToString()).FirstOrDefault().setting_value.ToString());
                //Percentage of horses required in the race with sufficient data to continue
                var variance = (listOfHorses.Count() * a);
                if (listOfHorses.Where(x => x.top_speed != null || x.top_speed > 0).Count() < variance)
                {
                    return -1;
                }

                var predictions = TopSpeed.CalculateResultsByTopSpeed(listOfHorses).Take(total);
                var results = race.RaceHorses.Where(x => x.position > 0).OrderBy(x => x.position).ToList().Take(total);

                foreach (var prediction in predictions)
                {
                    //If the predictions top 3 are in the results top 3 add a point
                    if (results.Any(x => x.horse_id == prediction.horse_id))
                    {
                        counter++;
                    }
                }
            }

            return counter / total * 100;
        }

        public async Task<List<HorseEntity>> TopSpeedVariablePredictions(RaceEntity race, List<AlgorithmSettingsEntity> settings)
        {
            var result = new List<HorseEntity>();

            var placed = SharedCalculations.GetTake(race.no_of_horses ?? 0);
            var listOfHorses = new List<HorseEntity>();

            foreach (var h in race.RaceHorses)
            {
                var even = race.Event;
                var horse = h.Horse;

                //GETTING ARCHIVED HORSE DATA
                var ts = await _raceService.GetTsForHorseRace(h.Horse.Archive, even.created);

                if (ts != -1)
                {
                    horse.top_speed = ts;
                }

                listOfHorses.Add(horse);
            }

            if (listOfHorses.Count > 0)
            {
                var a = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horsesrequired.ToString()).FirstOrDefault().setting_value.ToString());
                //Percentage of horses required in the race with sufficient data to continue
                var variance = (listOfHorses.Count() * a);
                if (listOfHorses.Where(x => x.top_speed != null || x.top_speed > 0).Count() < variance)
                {
                    return result;
                }

                var predictions = TopSpeed.CalculateResultsByTopSpeed(listOfHorses).Take(placed);

                foreach (var prediction in predictions)
                {
                    result.Add(prediction);
                }
            }

            return result;
        }
    }
}
