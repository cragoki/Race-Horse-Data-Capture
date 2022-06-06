namespace Infrastructure.PunterAdmin.ViewModels
{
    public class EventViewModel
    {
        public int RaceId { get; set; }
        public string Course { get; set; }
        public string SurfaceType { get; set; }
        public string Name { get; set; }
        public string MeetingURL { get; set; }
        public string MeetingType { get; set; }
        public int NumberOfRaces { get; set; }
    }
}
