using Core.Entities;
using Core.Models.Algorithm;
using Infrastructure.PunterAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Algorithms
{
    public interface IMyModel
    {
        Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races);
        Task<List<HorsePredictionModel>> GetHorsePoints(List<RaceHorseEntity> horses, RaceEntity race);
        Task<List<FormResultModel>> RunModel(RaceEntity race);
    }
}