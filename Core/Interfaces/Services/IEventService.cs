using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetTodaysEvents(Guid batch);
        Task<bool> GetBacklogEvents(Guid batch, DateTime date);
        Task<List<EventEntity>> GetEventsFromDatabase();
    }
}