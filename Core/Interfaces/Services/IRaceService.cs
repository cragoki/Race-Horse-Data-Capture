using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IRaceService
    {
        Task GetEventRaces(int EventId);
    }
}