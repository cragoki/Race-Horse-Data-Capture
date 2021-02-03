using ProjectPunter.Models;
using ProjectPunter.Models.Race;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static RaceViewModel BuildRaceModel(List<RaceHorseModel> raceHorses, RaceModel raceInfo) 
        {

            var result = new RaceViewModel()
            { 
            RaceHorses = raceHorses,
            RaceModel = raceInfo
            };

            return result;
        }

        public static DataTable BuildRaceResultDataTable(RaceViewModel model) 
        {
            //Declare table and add each column
            var result = new DataTable();
            result.Columns.Add("horse_id", typeof(int));
            result.Columns.Add("position", typeof(int));
            result.Columns.Add("dnf", typeof(bool));
            result.Columns.Add("clean_race", typeof(bool));

            //Add data to table foreach horse
            foreach (var horse in model.RaceHorses) 
            {
                result.Rows.Add(horse.Horse_Id, horse.Position, horse.DNF, horse.Clean_Race);
            }

            return result;
        }

    }
}