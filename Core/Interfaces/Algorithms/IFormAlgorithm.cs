using Core.Entities;
using Core.Models.Algorithm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Algorithms
{
    public interface IFormAlgorithm
    {
        Task<double> FormCalculation(RaceEntity race, List<AlgorithmVariableEntity> variables);
        Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races, List<AlgorithmVariableEntity> algorithms);
    }
}