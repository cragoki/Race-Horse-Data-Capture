using System;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class RaceHorseTrackerViewModel
    {
        public int Id { get; set; }
        public int RaceHorseId { get; set; }
        public decimal TotalPoints { get; set; }
        public decimal TotalPointsForGetCurrentCondition { get; set; }
        public decimal TotalPointsForLastTwoRaces { get; set; }
        public decimal TotalPointsForTimeSinceLastRace { get; set; }
        public string GetCurrentConditionDescription { get; set; }
        public decimal TotalPointsForPastPerformance { get; set; }
        public string GetPastPerformanceDescription { get; set; }
        public decimal TotalPointsForAdjustmentsPastPerformance { get; set; }
        public decimal TotalPointsForStrengthOfCompetition { get; set; }
        public decimal TotalPointsForWeight { get; set; }
        public decimal TotalPointsForJockeyContribution { get; set; }
        public string GetPastPerformanceAdjustmentsDescription { get; set; }
        public decimal PointsGivenForJockey { get; set; }
        public decimal PointsGivenForTrainer { get; set; }
        public string GetPresentRaceFactorsDescription { get; set; }
        public decimal TotalPointsForSpecificTrack { get; set; }
        public decimal TotalPointsForDistance { get; set; }
        public decimal TotalPointsForRaceType { get; set; }
        public decimal TotalPointsForGoing { get; set; }
        public string GetHorsePreferencesDescription { get; set; }
        public DateTime Created { get; set; }
    }
}
