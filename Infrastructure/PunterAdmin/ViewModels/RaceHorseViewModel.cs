namespace Infrastructure.PunterAdmin.ViewModels
{
    public class RaceHorseViewModel
    {
        public int RaceHorseId { get; set; }
        public string Name { get; set; }
        public int RaceId { get; set; }
        public int HorseId { get; set; }
        public string Weight { get; set; }
        public int Age { get; set; }
        public string TrainerName { get; set; }
        public string JockeyName { get; set; }
        public int Position { get; set; }
        public int? Ts { get; set; }
        public int? RPR { get; set; }

        public int? PredictedPosition { get; set; }
        public string Description { get; set; }
        public decimal? Points { get; set; }
        public string PointsDescription { get; set; }
        public string HorseReliability { get; set; }

    }
}
