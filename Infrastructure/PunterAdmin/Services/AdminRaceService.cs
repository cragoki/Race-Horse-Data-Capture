
using Core.Interfaces.Data.Repositories;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public class AdminRaceService
    {
        private static IConfigurationRepository _configRepo;
        private static IEventRepository _eventRepository;

        public AdminRaceService(IConfigurationRepository configRepo, IEventRepository eventRepository) 
        {
            _configRepo = configRepo;
            _eventRepository = eventRepository;
        }

        public async Task<List<TodaysRacesViewModel>> GetTodaysRaces() 
        {
            var result = new List<TodaysRacesViewModel>();
            var surface = "";
            var meeting = "";
            try
            {
                //Get Newest entry from tb_batch
                var batch = _configRepo.GetMostRecentBatch();

                //Get Events linked to that batch
                var events = _eventRepository.GetEventsByBatch(batch.batch_id);

                //foreach event in events
                foreach (var ev in events) 
                {
                    var races = new List<TodaysRaceViewModel>();
                    //Get races for event

                    var course = _eventRepository.GetCourseById(ev.course_id);
                    if (ev.surface_type != null) 
                    {
                         surface = _eventRepository.GetSurfaceTypeById(ev.surface_type ?? 0).surface_type;
                    }
                    if (ev.meeting_type != null)
                    {
                        meeting = _eventRepository.GetMeetingTypeById(ev.meeting_type ?? 0).meeting_type;
                    }
                    //buildTodaysRaceViewModel item foreach race
                    var toAdd = new TodaysRacesViewModel()
                    {
                        EventId = ev.event_id,
                        EventName = ev.name,
                        Track = course.name,
                        MeetingType = meeting,
                        SurfaceType = surface,
                        EventRaces = races,
                        NumberOfRaces = races.Count()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
