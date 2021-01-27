using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.Models.Race
{
    public class RaceHorseModel
    {

        public int Race_Id { get; set; }

        public int Horse_Id { get; set; }

        public string Horse_Name { get; set; }

        public int Weight { get; set; }

        public int Age { get; set; }

        public string Trainer_Name { get; set; }
        public string Jockey_Name { get; set; }
        public int? Position { get; set; }
        public bool? DNF { get; set; }

        public bool? Clean_Race { get; set; }
    }
}