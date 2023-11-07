using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Data.Repositories
{ 
    public interface IMappingTableRepository
    {
        string GetAgeType(int age);
        string GetDistanceType(int distance);
        List<DistanceType> GetDistanceTypes();
        string GetGoingType(int going);
        List<GoingType> GetGoingTypes();
        string GetMeetingType(int meeting);
        string GetStallsType(int stalls);
        string GetWeatherType(int weather);
        string GetSurfaceType(int surface);
        Task<int?> AddOrReturnAgeType(string age);
        Task<int?> AddOrReturnDistanceType(string distance);
        Task<int?> AddOrReturnGoingType(string going);
        Task<int?> AddOrReturnMeetingType(string meeting);
        Task<int?> AddOrReturnStallsType(string stalls);
        Task<int?> AddOrReturnSurfaceType(string surface);
        Task<int?> AddOrReturnWeatherType(string weather);
        Task SaveChanges();
    }
}