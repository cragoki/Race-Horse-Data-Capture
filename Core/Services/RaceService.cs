﻿using Core.Entities;
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

        public RaceService(IScraperService scraperService, IEventRepository evenRepository, IHorseRepository horseRepository)
        {
            _scraperService = scraperService;
            _eventRepository = evenRepository;
            _horseRepository = horseRepository;
        }

        public async Task GetEventRaces(int EventId)
        {
            string eventName = $"eventName - {DateTime.Now.Date}";

            try
            {
                var even = _eventRepository.GetEventById(EventId);

                if (even != null) 
                {
                    Logger.Info($"Fetching Races for event {even.name}.");

                    //Get all of the raceUrls/Weather/course Url for this event
                    var races = await _scraperService.RetrieveRacesForEvent(even);

                    //Add course URL if one does not already exist
                    var course = _eventRepository.GetCourseById(even.course_id);
                    if (string.IsNullOrEmpty(course.course_url)) 
                    {
                        course.course_url = races.CourseUrl;
                        course.rp_course_id = Int32.Parse(Regex.Match(course.course_url, @"\d+").Value);
                        _eventRepository.UpdateCourse(course);
                    }

                    //Add Each race
                    foreach (var race in races.RaceEntities) 
                    {
                        //Add to database
                        _eventRepository.AddRace(race);

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
                await _scraperService.GetResultsForRace(race, raceHorseList.ToList());

                race.completed = true;

                _eventRepository.UpdateRace(race);
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
    }
}
