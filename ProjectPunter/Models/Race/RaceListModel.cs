using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.Models.Race
{
    public class RaceListModel
    {
        public int Race_Id { get; set; }
        public DateTime Date { get; set; }
        public string Event_Name { get; set; }
        public int Class_Number { get; set; }
        public string Race_Type_Description { get; set; }
        public int Number_Of_Horses { get; set; }
        public bool isComplete { get; set; }
    }
}