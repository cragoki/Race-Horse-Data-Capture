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


    }
}
