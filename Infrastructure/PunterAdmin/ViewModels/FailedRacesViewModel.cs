namespace Infrastructure.PunterAdmin.ViewModels
{
    public class FailedRacesViewModel
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public string Description { get; set; }
        public string RaceUrl { get; set; }
        public int Attempts { get; set; }
    }
}
