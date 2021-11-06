namespace Core.Interfaces.Data.Repositories
{ 
    public interface IMappingTableRepository
    {
        int AddOrReturnAgeType(string age);
        int AddOrReturnDistanceType(string distance);
        int AddOrReturnGoingType(string going);
        int AddOrReturnMeetingType(string meeting);
        int AddOrReturnStallsType(string stalls);
        int AddOrReturnSurfaceType(string surface);
        int AddOrReturnWeatherType(string weather);
        void SaveChanges();
    }
}