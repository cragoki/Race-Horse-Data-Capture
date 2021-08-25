using Core.Entities;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventEntity> GetEvents();
        EventEntity GetEventById(int eventId);
        CourseEntity GetCourseById(int courseId);
        EventEntity GetEventByBatch(int courseId, Guid batch);
        IEnumerable<EventEntity> GetEventByCourse(int courseId);
        void AddCourse(CourseEntity courseToAdd);
        void UpdateCourse(CourseEntity courseToUpdate);
        void AddEvent(EventEntity eventToAdd);
        List<CourseEntity> GetCourses();
        void AddRace(RaceEntity raceToUpdate);
        void UpdateRace(RaceEntity raceToUpdate);
        void SaveChanges();
    }
}