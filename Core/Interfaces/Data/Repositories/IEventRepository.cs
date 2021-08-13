using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IEventRepository
    {
        void AddCourse(CourseEntity courseToAdd);
        void AddEvent(EventEntity eventToAdd);
        List<CourseEntity> GetCourses();
    }
}