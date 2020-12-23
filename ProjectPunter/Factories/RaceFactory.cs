using ProjectPunter.Models;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.Factories
{
    public static class RaceFactory
    {

        public static List<tb_race_horse> BuildRaceHorseModel(AddRaceHorseViewModel viewModel, List<tb_horse> horses)
        {
            var result = new List<tb_race_horse>();

            foreach (var raceHorse in viewModel.Result)
            {
                var thisHorse = horses.Where(x => x.Horse_Id == raceHorse.Horse_Id).FirstOrDefault();
                var raceId = raceHorse.Race_Id;
                var horse = new tb_race_horse()
                {
                    Horse_Id = raceHorse.Horse_Id,
                    Weight = raceHorse.Weight,
                    Age = thisHorse.Age ?? 0,
                    Trainer_Id = raceHorse.Trainer_Id,
                    Jockey_Id = raceHorse.Jockey_Id,
                    Race_Id = raceId
                };

                result.Add(horse);
            }

            return result;
        }
    }
}