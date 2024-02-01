using Core.Entities;
using Core.Models;
using Core.Models.GetRace;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScraperService
    {
        Task<DailyRaces> RetrieveTodaysEvents();
        Task<DailyRaces> RetrieveBacklogEvents(DateTime date);
        Task<List<RaceHorseModel>> RetrieveHorseDetailsForRace(RaceEntity race);
        Task<List<RaceHorseEntity>> GetResultsForRace(RaceEntity race, List<RaceHorseEntity> raceHorses);
        Task<RaceHorseEntity> GetResultsForRaceHorse(RaceHorseEntity raceHorse);
        Task<List<EventEntity>> GetEvents(string url);
        Task<EventEntity> GetEvent(HtmlNode course);
    }
}