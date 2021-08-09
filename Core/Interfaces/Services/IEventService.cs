using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IEventService
    {
        List<Event> GetTodaysEvents();
    }
}