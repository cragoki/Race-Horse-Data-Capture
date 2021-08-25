using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContextData _context;

        public EventRepository(DbContextData context)
        {
            _context = context;
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
            _context.SaveChanges();
        }
        public void AddCourse(CourseEntity courseToAdd)
        {
            _context.tb_course.Add(courseToAdd);
            _context.SaveChanges();
        }
        public void UpdateCourse(CourseEntity courseToUpdate)
        {
            _context.tb_course.Update(courseToUpdate);
            _context.SaveChanges();
        }

        public void AddRace(RaceEntity raceToAdd)
        {
            _context.tb_race.Add(raceToAdd);
            _context.SaveChanges();
        }
        public void UpdateRace(RaceEntity raceToUpdate)
        {
            _context.tb_race.Add(raceToUpdate);
            _context.SaveChanges();
        }

        public void SaveChanges() 
        {
            _context.SaveChanges();
        }
    }
}
