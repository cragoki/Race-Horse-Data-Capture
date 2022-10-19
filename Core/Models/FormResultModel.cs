using Core.Entities;
using Core.Enums;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class FormResultModel
    {
        public HorseEntity Horse { get; set; }
        public int RaceHorseId { get; set; }
        public decimal? Predictability { get; set; }
        public decimal? Points { get; set; }
        public string PointsDescription { get; set; }
    }
}
