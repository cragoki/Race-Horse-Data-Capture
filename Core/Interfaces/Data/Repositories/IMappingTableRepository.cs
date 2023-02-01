﻿using Core.Entities;
using System.Collections.Generic;

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
        int? AddOrReturnAgeType(string age);
        int? AddOrReturnDistanceType(string distance);
        int? AddOrReturnGoingType(string going);
        int? AddOrReturnMeetingType(string meeting);
        int? AddOrReturnStallsType(string stalls);
        int? AddOrReturnSurfaceType(string surface);
        int? AddOrReturnWeatherType(string weather);
        void SaveChanges();
    }
}