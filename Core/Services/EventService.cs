using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.GetRace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IScraperService _scraperService;
        private static IEventRepository _eventRepository;
        private static IMappingTableRepository _mappingTableRepository;

        public EventService(IScraperService scraperService, IEventRepository eventRepository, IMappingTableRepository mappingTableRepository)
        {
            _scraperService = scraperService;
            _eventRepository = eventRepository;
            _mappingTableRepository = mappingTableRepository;
        }

        public async Task<List<Event>> GetTodaysEvents(Guid batch)
        {
            var result = new List<Event>();
            var races = new List<Event>();

            try
            {
                Logger.Info("Fetching Todays Events.");

                var todaysRaces = await _scraperService.RetrieveTodaysEvents();

                //Return necessary information
                foreach (var even in todaysRaces.Courses)
                {
                    await AddDbInfoForEventAndRaces(even, batch);

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
        public async Task<bool> GetBacklogEvents(Guid batch, DateTime date)
        {
            await _scraperService.RetrieveBacklogEvents(date);
            return false;
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

        public async Task<IEnumerable<RaceEntity>> GetRacesFromDatabaseForAlgorithm(int event_id)
        {
            return await _eventRepository.GetRacesForEvent(event_id);
        }

        public async Task<IEnumerable<RaceEntity>> GetRacesForTodayToTest()
        {
            return _eventRepository.GetTodaysRacesTest();
        }

        private async Task AddDbInfoForEventAndRaces(Course even, Guid batchId)
        {
            var eventName = $"{even.Name}_{DateTime.Now.ToShortDateString()}";
            int? surfaceTypeId = null;
            int? meetingTypeId = null;

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

                await CheckAndAddCourse(course);

                if (!String.IsNullOrEmpty(even.SurfaceType))
                {
                    surfaceTypeId = await _mappingTableRepository.AddOrReturnSurfaceType(even.SurfaceType);
                }
                if (!String.IsNullOrEmpty(even.meetingTypeCode))
                {
                    meetingTypeId = await _mappingTableRepository.AddOrReturnMeetingType(even.meetingTypeCode);
                }

                //Event:
                var ev = new EventEntity()
                {
                    course_id = even.Id,
                    abandoned = even.Abandoned,
                    surface_type = surfaceTypeId,
                    name = eventName,
                    meeting_url = even.MeetingUrl,
                    hash_name = even.HashName,
                    meeting_type = meetingTypeId,
                    races = even.Races.Count,
                    created = DateTime.Now,
                    batch_id = batchId
                };

                int eventId = await _eventRepository.AddEvent(ev);

                if (eventId == 0)
                {
                    eventId = _eventRepository.GetEventByCourseAndBatch(ev.course_id, ev.batch_id).event_id;
                }
                if (eventId == 0)
                {
                    throw new Exception($"Failed to retrieve event {eventId} - {even.Name}");
                }

                await CheckAndAddCourse(course);

                if (even.Races.Count() == 0)
                {
                    throw new Exception($"Failed to retrieve races for event {eventId} - {even.Name}");
                }

                foreach (var race in even.Races)
                {
                    var raceEntity = new RaceEntity()
                    {
                        event_id = eventId,
                        no_of_horses = race.Runners,
                        race_class = race.RaceClass,
                        distance = await _mappingTableRepository.AddOrReturnDistanceType(race.Distance),
                        ages = await _mappingTableRepository.AddOrReturnAgeType(race.Ages),
                        description = "",
                        completed = false,
                        going = await _mappingTableRepository.AddOrReturnGoingType(race.Going),
                        race_time = race.Time,
                        race_url = race.RaceURL,
                        weather = await _mappingTableRepository.AddOrReturnWeatherType(race.Weather),
                        rp_race_id = race.Id
                    };

                    await _eventRepository.AddRace(raceEntity);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to add entity to the database for event {eventName}. Error: {ex.InnerException}");
            }

        }

        public async Task<List<EventEntity>> GetRandomEventsFromDatabase()
        {
            var result = new List<EventEntity>();

            try
            {
                var batch = _eventRepository.GetRandomBatch();
                result = _eventRepository.GetEventsByBatch(batch.batch_id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error trying to retrieve a random batch {ex.Message}");
            }

            return result;
        }

        public async Task<List<EventEntity>> GetLastTwoMonthsEvents()
        {
            var result = new List<EventEntity>();

            try
            {
                result = _eventRepository.GetLastTwoMonthsEvents().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error trying to retrieve events {ex.Message}");
            }

            return result;
        }

        private async Task CheckAndAddCourse(CourseEntity course)
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
                    await _eventRepository.AddCourse(course);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Error attempting to store course {course.name}.. {ex.Message} !!!");
            }
        }

        private void CheckAndAddDistance(CourseEntity course)
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
