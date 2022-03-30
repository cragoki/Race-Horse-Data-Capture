using Infrastructure.PunterAdmin.ViewModels;
using Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Infrastructure.PunterAdmin.Services
{
    public class AlgorithmService
    {
        private IAlgorithmRepository _algorithmRepository;
        private IConfigurationRepository _configurationRepository;

        public AlgorithmService(IAlgorithmRepository algorithmRepository, IConfigurationRepository configurationRepository) 
        {
            _algorithmRepository = algorithmRepository;
            _configurationRepository = configurationRepository;
    }

        public async Task<List<AlgorithmTableViewModel>> GetAlgorithmTableData() 
        {
            var result = new List<AlgorithmTableViewModel>();

            try
            {
                var algorithms = _algorithmRepository.GetAlgorithms();

                foreach (var algorithm in algorithms) 
                {

                    var settings = await GetAlgorithmSettings(algorithm.algorithm_id);
                    var variables = await GetAlgorithmVariables(algorithm.algorithm_id);

                    var toAdd = new AlgorithmTableViewModel()
                    {
                        AlgorithmId = algorithm.algorithm_id,
                        AlgorithmName = algorithm.algorithm_name,
                        Accuracy = algorithm.accuracy ?? 0,
                        IsActive = algorithm.active,
                        NumberOfRaces = algorithm.number_of_races,
                        Settings = settings,
                        Variables = variables
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }


        #region private
        private async Task<List<AlgorithSettingsTableViewModel>> GetAlgorithmSettings(int algorithmId) 
        {
            var result = new List<AlgorithSettingsTableViewModel>();
            var settings = _configurationRepository.GetAlgorithmSettings(algorithmId);


            foreach (var setting in settings)
            {
                result.Add(new AlgorithSettingsTableViewModel()
                {
                    AlgorithmSettingId = setting.algorithm_setting_id,
                    SettingName = setting.setting_name,
                    SettingValue = setting.setting_value
                });
            }

            return result;
        }

        private async Task<List<AlgorithmVariableTableViewModel>> GetAlgorithmVariables(int algorithmId)
        {
            var result = new List<AlgorithmVariableTableViewModel>();
            var algorithmVariables =  _algorithmRepository.GetAlgorithmVariableByAlgorithmId(algorithmId);


            foreach (var algorithmVariable in algorithmVariables)
            {
                var variable = _algorithmRepository.GetVariableById(algorithmVariable.variable_id);
                result.Add(new AlgorithmVariableTableViewModel()
                {
                    AlgorithmVariableId = algorithmVariable.algorithm_variable_id,
                    Threshold = algorithmVariable.threshold,
                    VariableName = variable.variable_name,
                });
            }

            return result;
        }
        #endregion
    }
}
