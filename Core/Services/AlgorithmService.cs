
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Variables;
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
        private static IHorseRepository _horseRepository;

        public AlgorithmService(IEventRepository eventRepository, IAlgorithmRepository algorithmRepository, IHorseRepository horseRepository)
        {
            _eventRepository = eventRepository;
            _algorithmRepository = algorithmRepository;
            _horseRepository = horseRepository;
        }

        public async Task<AlgorithmResult> ExecuteActiveAlgorithm()
        {
            var result = new AlgorithmResult();
            var runningTotal = new List<decimal>();
            try
            {
                var activeAlgorithm = _algorithmRepository.GetActiveAlgorithm();
                if (activeAlgorithm != null)
                {
                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                    var algorithmVariables = _algorithmRepository.GetAlgorithmVariableByAlgorithmId(activeAlgorithm.algorithm_id);
                    var races = _eventRepository.GetAllRaces();
                    result.RacesFiltered = races.Count();
                    foreach (var race in races)
                    {
                        var percentageCorrect = new List<decimal>();

                        foreach (var variable in algorithmVariables)
                        {
                            switch (variable.variable_id)
                            {
                                case (int)VariableEnum.TopSpeed:
                                    percentageCorrect.Add(await TopSpeedVariable(race) * variable.threshold);
                                    break;
                            }
                        }
                        //FOREACH RACE, RUN THE VARIABLE CLASS FROM THE VARIABLE DEFINED IN ALGORITHM VARIABLES
                        //CALCULATE AVERAGE RESULT OF EACH RACE AND UPDATE THE ACCURACY OF THE ALGORITHM
                        runningTotal.AddRange(percentageCorrect);
                    }
                    result.Accuracy = runningTotal.Average();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task StoreAlgorithmResults(AlgorithmResult result)
        {
            try
            {
                var update = _algorithmRepository.GetAlgorithmById(result.AlgorithmId);

                update.accuracy = result.Accuracy;
                update.number_of_races = result.RacesFiltered;
                update.active = false;

                _algorithmRepository.UpdateActiveAlgorithm(update);
            }
            catch (Exception ex) 
            {
                Logger.Error($"Error trying to store Algorithm result...{ex.Message}");
            }
        }

            private async Task<decimal> TopSpeedVariable(RaceEntity race)
        {
            var total = 3; // 3 horses placed in the prediction
            var counter = 0;
            var horses = _eventRepository.GetRaceHorsesForRace(race.race_id);
            var listOfHorses = new List<HorseEntity>();
            foreach (var h in horses)
            {
                var horse = _horseRepository.GetHorse(h.horse_id);
                listOfHorses.Add(horse);
            }

            if (listOfHorses.Count > 0)
            {
                var predictions = TopSpeed.CalculateResultsByTopSpeed(listOfHorses).Take(3);
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
