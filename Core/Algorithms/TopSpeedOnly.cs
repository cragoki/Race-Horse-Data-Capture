
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
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
        private readonly IEventRepository _eventRepository;
        private readonly IHorseRepository _horseRepository;
        private readonly IConfigurationRepository _configRepository;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TopSpeedOnly(IEventRepository eventRepository, IHorseRepository horseRepository, IConfigurationRepository configRepository) 
        {
            _eventRepository = eventRepository;
            _horseRepository = horseRepository;
            _configRepository = configRepository;
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
                    var predictionResult = await TopSpeedVariable(race);

                    if(predictionResult != 500)
                    {
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

        public async Task<double> TopSpeedVariable(RaceEntity race)
        {
            var settings = _configRepository.GetAlgorithmSettings((int)AlgorithmEnum.TopSpeedOnly);
            var total = 3; // 3 horses placed in the prediction
            var counter = 0;
            var horses = _eventRepository.GetRaceHorsesForRace(race.race_id);
            var listOfHorses = new List<HorseEntity>();
            //Percentage of horses required in the race with sufficient data to continue
            var variance = (Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horsesrequired.ToString()).FirstOrDefault().setting_value.ToString()) / listOfHorses.Count()) * 100;

            if (listOfHorses.Where(x => x.top_speed != null || x.top_speed > 0).Count() < variance) 
            {
                return 500;
            }

            foreach (var h in horses)
            {
                var horse = _horseRepository.GetHorse(h.horse_id);
                listOfHorses.Add(horse);
            }

            if (listOfHorses.Count > 0)
            {
                var predictions = TopSpeed.CalculateResultsByTopSpeed(listOfHorses).Take(total);
                var results = horses.Where(x => x.position > 0).OrderBy(x => x.position).ToList().Take(3);

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
    }
}
