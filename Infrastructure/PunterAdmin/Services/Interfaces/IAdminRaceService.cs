using Infrastructure.PunterAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public interface IAdminRaceService
    {
        Task<List<TodaysRacesViewModel>> GetTodaysRaces();
    }
}