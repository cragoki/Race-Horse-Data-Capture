using ProjectPunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.ViewModels
{
    public class AddRaceHorseViewModel
    {
        public int RaceId { get; set; }
        public List<tb_horse> HorseList { get; set; }
        public List<tb_jockey> JockeyList { get; set; }
        public List<tb_trainer> TrainerList { get; set; }
        public List<tb_country> CountryList { get; set; }
        public int NoOfHorses { get; set; }

        public List<tb_race_horse> Result { get; set; }
    }
}