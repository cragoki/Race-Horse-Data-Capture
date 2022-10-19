
using Core.Algorithms;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Variables;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static IEventRepository _eventRepository;
        private static IAlgorithmRepository _algorithmRepository;
        private static ITopSpeedOnly _topSpeedOnly;
        private static ITsRPR _topSpeedRpr;
        private static IFormAlgorithm _formAlgorithm;
        private static IFormRevamped _formRevampedAlgorithm;

        public AlgorithmService(IEventRepository eventRepository, IAlgorithmRepository algorithmRepository, ITopSpeedOnly topSpeedOnly, ITsRPR topSpeedRpr, IFormRevamped formRevampedAlgorithm, IFormAlgorithm formAlgorithm)
        {
            _eventRepository = eventRepository;
            _algorithmRepository = algorithmRepository;
            _topSpeedOnly = topSpeedOnly;
            _topSpeedRpr = topSpeedRpr;
            _formAlgorithm = formAlgorithm;
            _formRevampedAlgorithm = formRevampedAlgorithm;
        }

        public async Task<AlgorithmResult> ExecuteActiveAlgorithm()
        {
            var result = new AlgorithmResult();
            try
            {

                var activeAlgorithm = _algorithmRepository.GetActiveAlgorithm();

                if (activeAlgorithm != null)
                {
                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                    var algorithmVariables = _algorithmRepository.GetAlgorithmVariableByAlgorithmId(activeAlgorithm.algorithm_id);
                    var races = _eventRepository.GetAllRaces();

                    switch ((AlgorithmEnum)activeAlgorithm.algorithm_id) 
                    {
                        case AlgorithmEnum.TopSpeedOnly:
                            result = await _topSpeedOnly.GenerateAlgorithmResult(races);
                            break;
                        case AlgorithmEnum.TsRPR:
                            result = await _topSpeedRpr.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.FormOnly:
                            result = await _formAlgorithm.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.FormRevamp:
                            result = await _formRevampedAlgorithm.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                    }

                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service ExecuteActiveAlgorithm...{ex.Message}");
            }

            return result;
        }

        public AlgorithmEntity GetActiveAlgorithm() 
        {
            var result = new AlgorithmEntity();

            try
            {
                result = _algorithmRepository.GetActiveAlgorithm();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service GetActiveAlgorithm...{ex.Message}");
            }

            return result;
        }

        public async Task StoreAlgorithmResults(AlgorithmResult result)
        {
            try
            {
                var update = _algorithmRepository.GetAlgorithmById(result.AlgorithmId);

                update.accuracy = (decimal)result.Accuracy;
                update.number_of_races = result.RacesFiltered;
                update.active = false;

                _algorithmRepository.UpdateActiveAlgorithm(update);
            }
            catch (Exception ex) 
            {
                Logger.Error($"Error trying to store Algorithm result...{ex.Message}");
            }
        }

        public async Task<AlgorithmResult> ExecuteSelectedAlgorithm(List<RaceEntity> algorithm, List<AlgorithmEntity> events, List<AlgorithmVariableEntity> algorithmVariables )
        {
            var result = new AlgorithmResult();
            try
            {

                var activeAlgorithm = _algorithmRepository.GetActiveAlgorithm();

                if (activeAlgorithm != null)
                {
                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                    var races = _eventRepository.GetAllRaces();

                    switch ((AlgorithmEnum)activeAlgorithm.algorithm_id)
                    {
                        case AlgorithmEnum.TopSpeedOnly:
                            result = await _topSpeedOnly.GenerateAlgorithmResult(races);
                            break;
                        case AlgorithmEnum.TsRPR:
                            result = await _topSpeedRpr.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                        case AlgorithmEnum.FormOnly:
                            result = await _formAlgorithm.GenerateAlgorithmResult(races, algorithmVariables);
                            break;
                    }

                    result.AlgorithmId = activeAlgorithm.algorithm_id;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task<List<AlgorithmSettingsEntity>> GetSettingsForAlgorithm(int algorithm_id)
        {
            var result = new List<AlgorithmSettingsEntity>();

            try
            {
                var algorithm = _algorithmRepository.GetAlgorithms().Where(x => x.algorithm_id == algorithm_id).FirstOrDefault();

                result = algorithm.Settings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public void AddAlgorithmPrediction(AlgorithmPredictionEntity prediction) 
        {
            _algorithmRepository.AddAlgorithmPrediction(prediction);
        }

    }
}
