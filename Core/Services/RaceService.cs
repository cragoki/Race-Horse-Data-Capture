using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RaceService : IRaceService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;
        private readonly IEventRepository _eventRepository;
        private readonly IHorseRepository _horseRepository;
        private readonly IMappingTableRepository _mappingRepository;

        public RaceService(IScraperService scraperService, IEventRepository evenRepository, IHorseRepository horseRepository, IMappingTableRepository mappingRepository)
        {
            _scraperService = scraperService;
            _eventRepository = evenRepository;
            _horseRepository = horseRepository;
            _mappingRepository = mappingRepository;
        }

        public async Task GetEventRaces(int EventId)
        {

            try
            {
                var even = _eventRepository.GetEventById(EventId);

                if (even != null) 
                {
                    Logger.Info($"Fetching Races for event {even.name}.");

                    //Get all of the raceUrls/Weather/course Url for this event
                    var races = _eventRepository.GetRacesForEvent(EventId);

                    //Add Each race
                    foreach (var race in races) 
                    {
                        //Now use the race URL to fetch the Horses/Trainers/Owners
                        var horses = await _scraperService.RetrieveHorseDetailsForRace(race);

                        //if they dont then add them into tb_horse
                        foreach (var raceHorse in horses) 
                        {
                            var rh = raceHorse.RaceHorse;
                            _horseRepository.AddRaceHorse(rh);
                        }
                    }

                    Logger.Info($"Races Retrieved for event {even.name}");
                }

            }
            catch (Exception ex)
            {
                Logger.Error($"Error attempting to retrieve todays races.  {ex.Message}");
            }
        }

        public async Task<List<RaceEntity>> GetEventRacesFromDB(int EventId)
        {
            var result = new List<RaceEntity>();

            try
            {
                result = _eventRepository.GetRacesForEvent(EventId).ToList();
            }
            catch (Exception ex) 
            {
                Logger.Error($"Error attempting to retrieve todays races from the database.  {ex.InnerException}");
            }

            return result;
        }

        public async Task GetRaceResults(RaceEntity race) 
        {
            try
            {
                //Get RaceHorse Entities
                var raceHorseList = _horseRepository.GetRaceHorsesForRace(race.race_id);

                Logger.Info($"Found {raceHorseList.Count()} Horses for race...");
                Logger.Info($"Processing...");
                var raceHorses = await _scraperService.GetResultsForRace(race, raceHorseList.ToList());


                var raceDb = _eventRepository.GetRaceById(race.race_id);

                if (raceHorses != null || raceHorses.Count() != 0 && raceHorses.All(x => x.position != 0))
                {
                    raceDb.completed = true;
                }

                _eventRepository.UpdateRace(raceDb);
                Logger.Info($"Update Complete!");
                Logger.Info($"Finding next race...");

            }
            catch (Exception ex) 
            {
                Logger.Error($"Error attempting to retrieve race results.  {ex.Message}");
            }
        }

        public async Task<List<RaceEntity>> GetIncompleteRaces() 
        {
            var result = new List<RaceEntity>();

            try
            {
                //Get a list of races from the database where tb_race_horse.result = null AND where date is not today
                var nonCompleteRaces = _horseRepository.GetNoResultRaces();

                if (nonCompleteRaces != null && nonCompleteRaces.Count() > 0) 
                {
                    //Get race URL/Date
                    foreach (var race in nonCompleteRaces) 
                    {
                        var eventEntity = _eventRepository.GetEventById(race.event_id);

                        //Ensure that this is not a race which occurred today
                        if (eventEntity.created.Date != DateTime.Now.Date)
                        {
                            result.Add(race);
                        }
                        else 
                        {
                        
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Logger.Error($"Error attempting to retrieve incomplete races.  {ex.Message}");
            }

            return result;
        }

        public async Task<int> GetRprForHorseRace(List<HorseArchiveEntity> archive, DateTime raceDate) 
        {
            if (archive != null && archive.Count() != 0) 
            {
                //set the race date to equal that distance group
                var rpr = archive.Where(x => x.field_changed == "rpr" && x.date < raceDate)
                    .OrderByDescending(x => x.date).FirstOrDefault()?.new_value;

                if (rpr != "-") 
                {
                    if (Int32.TryParse(rpr, out int result)) 
                    {
                        return result;
                    }
                }
            }

            return -1;
        }

        public async Task<int> GetTsForHorseRace(List<HorseArchiveEntity> archive, DateTime raceDate)
        {
            if (archive != null && archive.Count() != 0)
            {
                var ts = archive.Where(x => x.field_changed == "ts" && x.date < raceDate)
                    .OrderByDescending(x => x.date).FirstOrDefault()?.new_value;

                if (ts != "-")
                {
                    if (Int32.TryParse(ts, out int result)) 
                    {
                        return result;
                    }
                }
            }

            return -1;
        }
    }
}
