using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IEventService
    {
        Task<List<RaceEntity>> GetRacesForTodayToTest();
        Task<List<Event>> GetTodaysEvents(Guid batch);
        Task<bool> GetBacklogEvents(Guid batch, DateTime date);
        Task<List<EventEntity>> GetEventsFromDatabase();
        Task<List<RaceEntity>> GetRacesFromDatabaseForAlgorithm(int event_id);
    }
}