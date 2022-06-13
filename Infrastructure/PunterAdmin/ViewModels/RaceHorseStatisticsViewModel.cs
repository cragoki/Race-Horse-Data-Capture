using System.Collections.Generic;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class RaceHorseStatisticsViewModel
    {
        public int HorseId { get; set; }
        public string HorseName { get; set; }
        public int? RPR { get; set; }
        public int? TS { get; set; }
        public int Age { get; set; }
        public string Weight { get; set; }
        public RaceHorseViewModel Horse { get; set; }
        public RaceViewModel CurrentRace { get; set; }
        public List<RaceViewModel> HorseRaces { get; set; }
        public List<TsRprAuditViewModel> TsRprAudit { get; set; }
    }
}
