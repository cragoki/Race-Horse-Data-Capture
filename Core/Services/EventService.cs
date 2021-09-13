using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.GetRace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;
        private static IEventRepository _eventRepository;
        public EventService(IScraperService scraperService, IEventRepository eventRepository)
        {
            _scraperService = scraperService;
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> GetTodaysEvents(Guid batch)
        {
            var result = new List<Event>();

            try
            {
                Logger.Info("Fetching Todays Events.");

                var todaysRaces = await _scraperService.RetrieveTodaysEvents();

                //Return necessary information
                foreach (var even in todaysRaces.Courses)
                {
                    AddDbInfoForEvent(even, batch);

                    //Now Collect the generated event_id based on the course and batch
                    var res = new Event()
                    {
                        EventId = _eventRepository.GetEventByBatch(even.Id, batch).event_id
                    };

                    result.Add(res);
                }

                Logger.Info($"{todaysRaces.Courses.Count} Events Retrieved and stored successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Error attempting to retrieve todays races. Terminating process. {ex.Message} !!!");
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<List<EventEntity>> GetEventsFromDatabase() 
        {
            var result = new List<EventEntity>();

            try
            {
                result = _eventRepository.GetTodaysEvents().ToList();
            }
            catch (Exception ex) 
            {
                Logger.Error($"Failed to get entities from the database. Error: {ex.InnerException}");
            }

            return result;
        }

        private void AddDbInfoForEvent(Course even, Guid batchId)
        {
            var eventName = $"{even.Name}_{DateTime.Now.ToShortDateString()}";
            try
            {
                //Store todays race information with batch
                //Course:
                var course = new CourseEntity()
                {
                    course_id = even.Id,
                    name = even.Name,
                    country_code = even.CountryCode,
                    all_weather = even.AllWeather
                };

                CheckAndAddCourse(course);

                //Event:
                var ev = new EventEntity()
                {
                    course_id = even.Id,
                    abandoned = even.Abandoned,
                    surface_type = even.SurfaceType,
                    name = eventName,
                    meeting_url = even.MeetingUrl,
                    hash_name = even.HashName,
                    meeting_type = even.meetingTypeCode,
                    races = even.Races.Count,
                    created = DateTime.Now,
                    batch_id = batchId
                };

                _eventRepository.AddEvent(ev);
            }
            catch (Exception ex) 
            {
                Logger.Error($"Failed to add entity to the database for event {eventName}. Error: {ex.InnerException}");
            }

        }

        private void CheckAndAddCourse(CourseEntity course) 
        {
            try
            {
                var courses = _eventRepository.GetCourses();

                if (courses.Any(x => x.course_id == course.course_id))
                {
                    Logger.Info($"{course.name} already exists in the database.");
                }
                else 
                {
                    _eventRepository.AddCourse(course);
                    _eventRepository.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                Logger.Error($"!!! Error attempting to store course {course.name}.. {ex.Message} !!!");
            }
        }

    }
}
