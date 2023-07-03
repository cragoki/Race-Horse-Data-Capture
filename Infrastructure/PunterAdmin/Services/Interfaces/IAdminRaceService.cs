using Core.Enums;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public interface IAdminRaceService
    {
        Task<List<TodaysRacesViewModel>> GetTodaysRaces(RaceRetrievalType retrievalType, Guid? batchId);
        Task<RaceHorseStatisticsViewModel> GetHorseStatistics(RaceHorseViewModel raceHorse, RaceViewModel race);
        Task RunResultRetrieval();
        Task<List<RaceStatisticViewModel>> GetHorseRaces(int horseId, int raceId);
    }
}