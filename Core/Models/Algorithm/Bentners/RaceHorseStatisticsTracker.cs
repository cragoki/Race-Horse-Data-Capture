using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Algorithm.Bentners
{
    public class RaceHorseStatisticsTracker
    {
        public decimal TotalPoints { get; set; }
        public decimal TotalPointsForGetCurrentCondition { get; set; }
        public decimal TotalPointsForLastTwoRaces { get; set; }
        public decimal TotalPointsForTimeSinceLastRace { get; set; }
        public string GetCurrentConditionDescription { get; set; }

    }
}
