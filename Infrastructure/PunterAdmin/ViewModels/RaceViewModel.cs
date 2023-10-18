using System;
using System.Collections.Generic;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class RaceViewModel
    {
        public int RaceId { get; set; }
        public bool AlgorithmRan { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string RaceTime { get; set; }
        public string Weather { get; set; }
        public string NumberOfHorses { get; set; }
        public string Going { get; set; }
        public string Stalls { get; set; }
        public string Distance { get; set; }
        public string RaceClass { get; set; }
        public string Ages { get; set; }
        public string Description { get; set; }
        public string RaceType { get; set; }
        public string RaceUrl { get; set; }
        public bool Completed { get; set; }
        public bool ShowHorses { get; set; }
        public bool ShowResults { get; set; }
        public decimal TotalPoints { get; set; }
        public decimal AveragePoints { get; set; }
        public decimal PointGap { get; set; }

        public List<RaceHorseViewModel> Horses { get; set; }
    }
}
