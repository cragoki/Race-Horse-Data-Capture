using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.Models.Race
{
    public class RaceModel
    {

        public int Race_Id { get; set; }

        public int Event_Id { get; set; }

        public string Weather_Description { get; set; }
        public string Surface_Description { get; set; }
        public string Race_Type_Description { get; set; }
        public string Class_Description { get; set; }
        public int Number_Of_Horses { get; set; }
        public bool IsComplete { get; set; }
        public DateTime Date { get; set; }

    }
}