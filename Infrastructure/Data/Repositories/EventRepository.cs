using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContextData _context;
        private readonly IConfigurationService _configService;

        public EventRepository(DbContextData context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService;
        }

        public IEnumerable<EventEntity> GetEvents()
        {
            return _context.tb_event.ToList();
        }
        public IEnumerable<EventEntity> GetTodaysEvents() 
        {
            return _context.tb_event.Where(x => x.created.Date == DateTime.Now.Date);
        }
        public List<CourseEntity> GetCourses()
        {
            return _context.tb_course.ToList();
        }
        public CourseEntity GetCourseById(int courseId)
        {
            return _context.tb_course.Where(x => x.course_id == courseId).FirstOrDefault();
        }
        public MeetingType GetMeetingTypeById(int meetingTypeId)
        {
            return _context.tb_meeting_type.Where(x => x.meeting_type_id == meetingTypeId).FirstOrDefault();
        }
        public SurfaceType GetSurfaceTypeById(int surfaceTypeId)
        {
            return _context.tb_surface_type.Where(x => x.surface_type_id == surfaceTypeId).FirstOrDefault();
        }
        public EventEntity GetEventById(int eventId)
        {
            return _context.tb_event.Find(eventId);
        }

        public EventEntity GetEventByBatch(int courseId, Guid batch) 
        {
            return _context.tb_event.Where(x => x.course_id == courseId && x.batch_id == batch).FirstOrDefault();
        }
        public List<EventEntity> GetEventsByBatch(Guid batch)
        {
            return _context.tb_event.Where(x => x.batch_id == batch).ToList();
        }
        public IEnumerable<EventEntity> GetEventByCourse(int courseId)
        {
            return _context.tb_event.Where(x => x.course_id == courseId).ToList();
        }
        public void AddEvent(EventEntity eventToAdd)
        {
            _context.tb_event.Add(eventToAdd);
            SaveChanges();
        }
        public void AddCourse(CourseEntity courseToAdd)
        {
            _context.tb_course.Add(courseToAdd);
            SaveChanges();
        }
        public void UpdateCourse(CourseEntity courseToUpdate)
        {
            _context.tb_course.Update(courseToUpdate);
            SaveChanges();
        }
        public IEnumerable<RaceEntity> GetRacesForEvent(int eventId)
        {
            return _context.tb_race.Where(x => x.event_id == eventId);
        }

        public RaceEntity GetRaceById(int raceId)
        {
            return _context.tb_race.Where(x => x.race_id == raceId).FirstOrDefault();
        }
        public List<RaceEntity> GetAllRaces()
        {
            return _context.tb_race.ToList();
        }
        public void AddRace(RaceEntity raceToAdd)
        {
            _context.tb_race.Add(raceToAdd);
            SaveChanges();
        }
        public void UpdateRace(RaceEntity raceToUpdate)
        {
            _context.tb_race.Update(raceToUpdate);
            SaveChanges();
        }

        public List<RaceHorseEntity> GetRaceHorsesForRace(int raceId) 
        {
            return _context.tb_race_horse.Where(x => x.race_id == raceId).ToList();
        }

        public void SaveChanges()
        {
            if (_configService.SavePermitted())
            {
                _context.SaveChanges();
            }
        }
    }
}
