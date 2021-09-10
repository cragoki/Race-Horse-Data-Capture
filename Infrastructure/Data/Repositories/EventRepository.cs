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
        public List<CourseEntity> GetCourses()
        {
            return _context.tb_course.ToList();
        }
        public CourseEntity GetCourseById(int courseId)
        {
            return _context.tb_course.Where(x => x.course_id == courseId).FirstOrDefault();
        }

        public EventEntity GetEventById(int eventId)
        {
            return _context.tb_event.Find(eventId);
        }

        public EventEntity GetEventByBatch(int courseId, Guid batch) 
        {
            return _context.tb_event.Where(x => x.course_id == courseId && x.batch_id == batch).FirstOrDefault();

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

        public void AddRace(RaceEntity raceToAdd)
        {
            _context.tb_race.Add(raceToAdd);
            SaveChanges();
        }
        public void UpdateRace(RaceEntity raceToUpdate)
        {
            _context.tb_race.Add(raceToUpdate);
            SaveChanges();
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
