
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Data.Repositories;
using Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class AlgorithmService
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

        public int ExecuteActiveAlgorithm() 
        {
            var result = 0;
            var runningTotal = new List<int>();
            try
            {
                var activeAlgorithm = _algorithmRepository.GetActiveAlgorithm();
                if (activeAlgorithm != null) 
                {
                    var algorithmVariables = _algorithmRepository.GetAlgorithmVariableByAlgorithmId(activeAlgorithm.algorithm_id);
                    var races = _eventRepository.GetAllRaces();

                    foreach (var race in races) 
                    {
                        var percentageCorrect = new List<decimal>();

                        foreach (var variable in algorithmVariables) 
                        {
                            switch (variable.variable_id) 
                            {
                                case (int)VariableEnum.TopSpeed:
                                        percentageCorrect.Add(TopSpeedVariable(race) * variable.threshold);
                                    break;
                            }
                        }
                        //FOREACH RACE, RUN THE VARIABLE CLASS FROM THE VARIABLE DEFINED IN ALGORITHM VARIABLES
                        //CALCULATE AVERAGE RESULT OF EACH RACE AND UPDATE THE ACCURACY OF THE ALGORITHM

                    }
                }
            }
            catch (Exception ex) 
            {
            
            }

            return result;
        }

        private decimal TopSpeedVariable(RaceEntity race) 
        {
            var horses = _eventRepository.GetRaceHorsesForRace(race.race_id);
            var listOfHorses = new List<HorseEntity>();
            foreach (var h in horses)
            {
                var horse = _horseRepository.GetHorse(h.horse_id);
                listOfHorses.Add(horse);
            }

            if (listOfHorses.Count > 0)
            {
                var predictions = TopSpeed.CalculateResultsByTopSpeed(listOfHorses);
                var results = horses.OrderBy(x => x.position).ToList();

            }

            return 0;
        }
    }
}
