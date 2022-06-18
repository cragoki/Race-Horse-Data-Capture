﻿using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Infrastructure.PunterAdmin.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            return _context.tb_event
                .Include(x => x.Course)
                .Include(x => x.MeetingType)
                .Include(x => x.Surface)
                .Include(x => x.Races)
                    .ThenInclude(y => y.RaceHorses)
                        .ThenInclude(z => z.Horse)
                            .ThenInclude(a => a.Archive)
                .Include(x => x.Races)
                    .ThenInclude(y => y.RaceHorses)
                        .ThenInclude(z => z.Jockey)
                .Include(x => x.Races)
                    .ThenInclude(y => y.RaceHorses)
                        .ThenInclude(z => z.Trainer)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Weather)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Stalls)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Distance)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Ages)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Going)
                .ToList();
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
            return _context.tb_event.Where(x => x.batch_id == batch)
                .Include(x => x.Course)
                .Include(x => x.MeetingType)
                .Include(x => x.Surface)
                .Include(x => x.Races)
                    .ThenInclude(y => y.RaceHorses)
                        .ThenInclude(z => z.Horse)
                            .ThenInclude(a =>a.Archive)
                .Include(x => x.Races)
                    .ThenInclude(y => y.RaceHorses)
                        .ThenInclude(z => z.Jockey)
                .Include(x => x.Races)
                    .ThenInclude(y => y.RaceHorses)
                        .ThenInclude(z => z.Trainer)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Weather)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Stalls)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Distance)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Ages)
                .Include(x => x.Races)
                    .ThenInclude(y => y.Going)
                .ToList();
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
            return _context.tb_race.Where(x => x.event_id == eventId)
                .Include(x => x.RaceHorses)
                    .ThenInclude(x => x.Horse)
                        .ThenInclude(x => x.Archive)
                .Include(x => x.Event);
        }

        //public async IQueryable<List<TodaysRacesViewModel>> GetEventAdminData(int eventId) 
        //{
        //    try
        //    {
        //        return await (from ev in _context.tb_event
        //                      join race in ev on ev.event_id equals race.)
        //    }
        //    catch (Exception ex) 
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public RaceEntity GetRaceById(int raceId)
        {
            return _context.tb_race.Where(x => x.race_id == raceId).FirstOrDefault();
        }
        public List<RaceEntity> GetAllRaces()
        {
            return _context.tb_race
                .Include(x => x.RaceHorses)
                    .ThenInclude(x => x.Horse)
                        .ThenInclude(x => x.Archive)
                 .Include(x => x.Going)
                 .Include(x => x.Weather)
                 .Include(x => x.Stalls)
                 .Include(x => x.Distance)
                 .Include(x => x.Event)
                 .ToList();
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
            return _context.tb_race_horse.Where(x => x.race_id == raceId).Include(x => x.Horse).ThenInclude(x => x.Archive).Include(x => x.Race).ThenInclude(x => x.Event).ToList();
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
