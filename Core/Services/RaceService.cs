using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfigurationRepository _configRepo;

        public RaceService(IScraperService scraperService, IEventRepository evenRepository, IHorseRepository horseRepository, IConfigurationRepository configRepo)
        {
            _scraperService = scraperService;
            _eventRepository = evenRepository;
            _horseRepository = horseRepository;
            _configRepo = configRepo;
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
                    var races = _eventRepository.GetRacesForEvent(EventId).ToList(); // PULL INTO MEM TO PREVENT TRACKING ERROR

                    //Add Each race
                    foreach (var race in races) 
                    {
                        try 
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
                        catch (Exception ex)
                        {
                            FailedRace(race, ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
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

                if (raceHorses != null && raceHorses.Count() != 0)
                {
                    if (raceHorses.All(x => x.position != 0))
                    {
                        raceDb.completed = true;
                        if (raceHorses.Any(x => x.position == -1))
                        {
                            foreach (var raceHorse in raceHorses.Where(x => x.position == -1))
                            {
                                //Check the Failed results table to remove this race_horse if result is retrieved successfully
                                var failedResult = new FailedResultEntity()
                                {
                                    race_horse_id = raceHorse.race_horse_id,
                                    error_message = raceHorse.description
                                };

                                raceHorse.description = "ERROR";
                                raceHorse.position = 0;

                                _configRepo.AddFailedResult(failedResult);


                                _horseRepository.UpdateRaceHorse(raceHorse);
                            }
                        }
                    }
                }

                _eventRepository.UpdateRace(raceDb);
                Logger.Info($"Update Complete!");
                Logger.Info($"Finding next race...");

            }
            catch (Exception ex) 
            {
                if (race.RaceHorses.Count() > 0)
                {
                    var failedResult = new FailedResultEntity()
                    {
                        race_horse_id = race.RaceHorses.FirstOrDefault().race_horse_id,
                        error_message = "Something went wrong collecting the results for this race"
                    };

                    _configRepo.AddFailedResult(failedResult);
                    Logger.Error($"Error attempting to retrieve race results.  {ex.Message}");
                }
                else 
                {
                    var failedResult = new FailedRaceEntity()
                    {
                        race_id = race.race_id,
                        error_message = "Something went wrong collecting the results for this race"
                    };

                    _configRepo.AddFailedRace(failedResult);
                    Logger.Error($"Error attempting to retrieve race results.  {ex.Message}");
                }

            }
        }

        public async Task<List<RaceHorseEntity>> GetIncompleteRaces() 
        {
            var results = new List<RaceHorseEntity>();
            try
            {
                var horses = _horseRepository.GetRaceHorseWithNoPosition();

                foreach (var horse in horses)
                {
                    try
                    {
                        var horseResult = await _scraperService.GetResultsForRaceHorse(horse);
                        results.Add(horseResult);
                    }
                    catch (Exception ex)
                    {

                        //Ignore for now
                    }
                }

                foreach (var raceHorse in results)
                {
                    //Check the Failed results table to remove this race_horse if result is retrieved successfully

                    if (raceHorse.position == -1)
                    {
                        var failedResult = new FailedResultEntity()
                        {
                            race_horse_id = raceHorse.race_horse_id,
                            error_message = raceHorse.description
                        };

                        raceHorse.description = "ERROR";
                        raceHorse.position = 0;

                        _configRepo.AddFailedResult(failedResult);
                    }

                    _horseRepository.UpdateRaceHorse(raceHorse);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return results;
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

        public async Task<int> GetMissingRaceData() 
        {
            var result = 0;

            try
            {
                var racesToRetry = _eventRepository.GetRacesWithMissingRaceHorses();

                foreach (var race in racesToRetry) 
                {
                    try
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
                    catch (Exception ex)
                    {
                        FailedRace(race, ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
                    }

                    result++;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private void FailedRace(RaceEntity race, string exception) 
        {
            var existing = _configRepo.GetFailedRace(race.race_id);
            if (existing == null)
            {
                var failedResult = new FailedRaceEntity()
                {
                    race_id = race.race_id,
                    error_message = exception
                };

                _configRepo.AddFailedRace(failedResult);
            }
            else
            {
                existing.attempts++;
                existing.error_message = exception;
                _configRepo.UpdateFailedRace(existing);
            }
        }
    }
}
