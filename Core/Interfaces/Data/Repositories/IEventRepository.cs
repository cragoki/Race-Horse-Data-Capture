using Core.Entities;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventEntity> GetEvents();
        IEnumerable<EventEntity> GetTodaysEvents();
        EventEntity GetEventById(int eventId);
        CourseEntity GetCourseById(int courseId);
        MeetingType GetMeetingTypeById(int meetingTypeId);
        SurfaceType GetSurfaceTypeById(int surfaceTypeId);
        EventEntity GetEventByBatch(int courseId, Guid batch);
        List<EventEntity> GetEventsByBatch(Guid batch);
        IEnumerable<RaceEntity> GetRacesForBatch(Guid batchId);
        IEnumerable<EventEntity> GetEventByCourse(int courseId);
        void AddCourse(CourseEntity courseToAdd);
        void UpdateCourse(CourseEntity courseToUpdate);
        void AddEvent(EventEntity eventToAdd);
        List<CourseEntity> GetCourses();
        List<RaceEntity> GetAllRaces();
        IEnumerable<RaceEntity> GetRacesForEvent(int eventId);
        void AddRace(RaceEntity raceToUpdate);
        void UpdateRace(RaceEntity raceToUpdate);
        RaceEntity GetRaceById(int raceId);
        List<RaceHorseEntity> GetRaceHorsesForRace(int raceId);
        void SaveChanges();
        List<EventEntity> TestAlgorithmWithOneHundredEvents();
    }
}