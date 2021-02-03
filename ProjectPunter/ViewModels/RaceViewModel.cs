using ProjectPunter.Models.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.ViewModels
{
    public class RaceViewModel
    {

        public RaceViewModel() 
        {
            this.RaceHorses = new List<RaceHorseModel>();
            this.RaceModel = new RaceModel();
        }

        public List<RaceHorseModel> RaceHorses { get; set; }
        public RaceModel RaceModel { get; set; }
    }
}