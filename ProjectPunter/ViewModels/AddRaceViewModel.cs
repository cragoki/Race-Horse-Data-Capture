using ProjectPunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.ViewModels
{
    public class AddRaceViewModel
    {
        public List<tb_event> EventList { get; set; }
        public List<tb_weather> WeatherList { get; set; }
        public List<tb_racetype> RaceType { get; set; }
        public List<tb_surface> SurfaceType { get; set; }
        public List<tb_class> ClassList { get; set; }

        public DateTime Date { get; set; }
        public int NumberOfHorses { get; set; }
    }
}