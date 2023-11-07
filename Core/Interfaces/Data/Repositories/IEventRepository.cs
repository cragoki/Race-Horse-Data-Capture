using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Data.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventEntity> GetEvents();
        IEnumerable<RaceEntity> GetTodaysRacesTest();
        IEnumerable<EventEntity> GetTodaysEvents();
        EventEntity GetEventById(int eventId);
        CourseEntity GetCourseById(int courseId);
        MeetingType GetMeetingTypeById(int meetingTypeId);
        SurfaceType GetSurfaceTypeById(int surfaceTypeId);
        EventEntity GetEventByBatch(int courseId, Guid batch);
        IQueryable<EventEntity> GetEventsByBatch(Guid batch);
        IEnumerable<RaceEntity> GetRacesForBatch(Guid batchId);
        IEnumerable<EventEntity> GetEventByCourse(int courseId);
        Task AddCourse(CourseEntity courseToAdd);
        Task UpdateCourse(CourseEntity courseToUpdate);
        Task<int> AddEvent(EventEntity eventToAdd);
        List<CourseEntity> GetCourses();
        List<RaceEntity> GetAllRaces();
        IEnumerable<RaceEntity> GetRacesForEvent(int eventId);
        Task AddRace(RaceEntity raceToUpdate);
        Task UpdateRace(RaceEntity raceToUpdate);
        RaceEntity GetRaceById(int raceId);
        RaceEntity GetRaceByURL(string raceURL);
        List<RaceHorseEntity> GetRaceHorsesForRace(int raceId);
        Task SaveChanges();
        List<EventEntity> TestAlgorithmWithOneHundredEvents();
        RaceEntity GetAllRaceDataById(int raceId);
        List<RaceEntity> GetRacesWithMissingRaceHorses();
        IEnumerable<RaceEntity> GetRacesForEventSimple(int eventId);
        BatchEntity GetRandomBatch();
        IQueryable<EventEntity> GetLastTwoMonthsEvents();
    }
}