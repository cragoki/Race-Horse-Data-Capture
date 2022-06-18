using System;
using System.Collections.Generic;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class TodaysRacesViewModel
    {
        public bool IsMostRecent { get; set; }
        public bool IsFirst { get; set; }
        public Guid BatchId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Track { get; set; }
        public string MeetingType { get; set; }
        public string SurfaceType { get; set; }
        public int NumberOfRaces { get; set; }
        public string MeetingURL { get; set; }
        public bool ShowRaces { get; set; }
        public List<RaceViewModel> EventRaces { get; set; }
    }
}
