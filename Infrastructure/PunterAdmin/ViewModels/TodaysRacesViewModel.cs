using System.Collections.Generic;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class TodaysRacesViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Track { get; set; }
        public string MeetingType { get; set; }
        public string SurfaceType { get; set; }
        public int NumberOfRaces { get; set; }
        public string MeetingURL { get; set; }
        public bool ShowRaces { get; set; }
        public List<TodaysRaceViewModel> EventRaces { get; set; }
    }
}
