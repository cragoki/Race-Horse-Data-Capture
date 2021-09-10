﻿using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RaceService : IRaceService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;
        private readonly IEventRepository _eventRepository;

        public RaceService(IScraperService scraperService, IEventRepository evenRepository)
        {
            _scraperService = scraperService;
            _eventRepository = evenRepository;
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
                        _eventRepository.UpdateCourse(course);
                    }

                    //Add Each race
                    foreach (var race in races.RaceEntities) 
                    {
                        //Add to database
                        _eventRepository.AddRace(race);

                        //Now use the race URL to fetch the Horses/Trainers/Owners
                        var horses = await _scraperService.RetrieveHorseDetailsForRace(race);

                        //another foreach, loop through the horses to check if they exist, if they do update/archive the details then continue
                        //if they dont then add them into tb_horse

                        //Now add all race horse entities
                    }

                    Logger.Info($"Races Retrieved for event {even.name}");
                }

            }
            catch (Exception ex)
            {
                Logger.Error($"Error attempting to retrieve todays races.  {ex.Message}");
            }
        }
    }
}
