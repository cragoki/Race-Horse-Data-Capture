using Core.Entities;
using Core.Enums;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class FormResultViewModel
    {
        public HorseEntity Horse { get; set; }
        public ReliabilityType Reliability { get; set; }
        public decimal? Points { get; set; }
    }
}
