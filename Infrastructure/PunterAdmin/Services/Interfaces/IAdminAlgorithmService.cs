using Infrastructure.PunterAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public interface IAdminAlgorithmService
    {
        Task<List<AlgorithmTableViewModel>> GetAlgorithmTableData();
    }
}