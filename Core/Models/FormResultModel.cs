using Core.Entities;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class FormResultModel
    {
        public int horse_id { get; set; }
        public int RaceHorseId { get; set; }
        public string Predictability { get; set; }
        public decimal? Points { get; set; }
        public string PointsDescription { get; set; }

        public AlgorithmTrackerEntity Tracker { get; set; }
    }
}
