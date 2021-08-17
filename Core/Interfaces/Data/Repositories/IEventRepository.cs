using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventEntity> GetEvents();
        EventEntity GetEventById(int eventId);
        IEnumerable<EventEntity> GetEventByCourse(int courseId);
        void AddCourse(CourseEntity courseToAdd);
        void AddEvent(EventEntity eventToAdd);
        List<CourseEntity> GetCourses();
        void SaveChanges();
    }
}