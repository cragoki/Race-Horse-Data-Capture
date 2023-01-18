using Core.Entities;
using Core.Models.Algorithm;
using Infrastructure.PunterAdmin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Algorithms
{
    public interface IBentnersModel
    {
        Task<List<FormResultModel>> RunModel(RaceEntity race);
        Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races);
    }
}