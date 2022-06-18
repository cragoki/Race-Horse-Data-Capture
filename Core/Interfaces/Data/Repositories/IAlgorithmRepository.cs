using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IAlgorithmRepository
    {
        AlgorithmEntity GetActiveAlgorithm();
        List<AlgorithmEntity> GetAlgorithms();
        AlgorithmEntity GetAlgorithmById(int algorithmId);
        List<AlgorithmVariableEntity> GetAlgorithmVariableByAlgorithmId(int algorithmId);
        AlgorithmVariableEntity GetAlgorithmVariableById(int algorithmVariableId);
        VariableEntity GetVariableById(int variableId);
        void SaveChanges();
        void UpdateActiveAlgorithm(AlgorithmEntity algorithmEntity);
        void UpdateAlgorithmSettings(List<AlgorithmSettingsEntity> algorithmSettings);
        void UpdateAlgorithmVariables(List<AlgorithmVariableEntity> algorithmVariables);
    }
}