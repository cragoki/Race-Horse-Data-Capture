﻿using System.Collections.Generic;

namespace Core.Models.Algorithm.Bentners
{
    public class RaceHorseStatisticsTracker
    {
        public decimal TotalPoints { get; set; }
        //CurrentCondition
        public decimal TotalPointsForGetCurrentCondition { get; set; }
        public decimal TotalPointsForLastTwoRaces { get; set; }
        public decimal TotalPointsForTimeSinceLastRace { get; set; }
        public string GetCurrentConditionDescription { get; set; }

        //PastPerformance
        public decimal TotalPointsForPastPerformance { get; set; }
        public string GetPastPerformanceDescription { get; set; }

        //GetAdjustmentsPastPerformance

        //GetPresentRaceFactors
        public decimal pointsForJockey { get; set; }
        public decimal pointsForTrainer { get; set; }
        public Dictionary<int, double> JockeyRankings { get; set; }
        public Dictionary<int, double> TrainerRankings { get; set; }


    }
}
