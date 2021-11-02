using Core.Models.Algorithm;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IAlgorithmService
    {
        Task<AlgorithmResult> ExecuteActiveAlgorithm();
        Task StoreAlgorithmResults(AlgorithmResult result);
    }
}