using Core.Entities;
using Core.Models.Algorithm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Algorithms
{
    public interface ITsRPR
    {
        Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races, List<AlgorithmVariableEntity> algorithms);
        Task<double> TSRpRCalculation(RaceEntity race, List<AlgorithmVariableEntity> variables);
    }
}