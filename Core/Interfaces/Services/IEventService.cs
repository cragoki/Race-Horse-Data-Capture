using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetTodaysEvents();
    }
}