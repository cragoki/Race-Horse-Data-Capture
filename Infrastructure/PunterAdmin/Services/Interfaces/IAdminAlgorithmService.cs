using Core.Entities;
using Infrastructure.PunterAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public interface IAdminAlgorithmService
    {
        Task<List<AlgorithmTableViewModel>> GetAlgorithmTableData();
        Task<List<TodaysRacesViewModel>> RunAlgorithm(AlgorithmTableViewModel algorithm, List<TodaysRacesViewModel> events);
        Task<AlgorithmTableViewModel> RunAlgorithmForAll(AlgorithmTableViewModel algorithm);
        Task<AlgorithmTableViewModel> UpdateAlgorithmSettings(AlgorithmTableViewModel algorithm);
        Task<AlgorithmTableViewModel> UpdateAlgorithmVariables(AlgorithmTableViewModel algorithm);
        Task<List<AlgorithmSettingsEntity>> BuildAlgorithmSettings(AlgorithmTableViewModel algorithm);
        Task<RaceViewModel> GetRacePredictionsForURL(int algorithmId, string raceUrl);
    }
}