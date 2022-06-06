
using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Infrastructure.Data;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.PunterAdmin.Services
{
    public class AdminRaceService : IAdminRaceService
    {
        private static IConfigurationRepository _configRepo;
        private static IEventRepository _eventRepository;
        private static IMappingTableRepository _mappingRepository;
        private static IHorseRepository _horseRepository;

        public AdminRaceService(IConfigurationRepository configRepo, IEventRepository eventRepository, IMappingTableRepository mappingRepository, IHorseRepository horseRepository)
        {
            _configRepo = configRepo;
            _eventRepository = eventRepository;
            _mappingRepository = mappingRepository;
            _horseRepository = horseRepository;
        }

        public async Task<List<TodaysRacesViewModel>> GetTodaysRaces()
        {
            var result = new List<TodaysRacesViewModel>();

            try
            {
                //Get Newest entry from tb_batch
                var batch = _configRepo.GetMostRecentBatch();

                //Get Events linked to that batch
                var events = _eventRepository.GetEventsByBatch(batch.batch_id);

                //foreach event in events
                foreach (var ev in events)
                {
                    var toAdd = new TodaysRacesViewModel()
                    {
                        EventId = ev.event_id,
                        EventName = ev.name,
                        Track = ev.Course?.name,
                        MeetingURL = $"https://www.racingpost.com/{ev.meeting_url}",
                        MeetingType = ev.MeetingType?.meeting_type,
                        SurfaceType = String.IsNullOrEmpty(ev.Surface?.surface_type) ? "Unknown" : ev.Surface?.surface_type,
                        EventRaces = BuildTodaysRaceViewModel(ev.Races), 
                        NumberOfRaces = ev.Races.Count()
                    };

                    result.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<TodaysRaceViewModel> BuildTodaysRaceViewModel(List<RaceEntity> races) 
        {
            var result = new List<TodaysRaceViewModel>();

            try
            {
                foreach (var race in races)
                {

                    var toAdd = new TodaysRaceViewModel()
                    {
                        RaceId = race.race_id,
                        Ages = race.Ages?.age_type,
                        Completed = race.completed,
                        Description = race.description,
                        Distance = race.Distance?.distance_type,
                        EventId = race.event_id,
                        Going = $"Going: {race.Going?.going_type}",
                        NumberOfHorses = $"{race.no_of_horses} Horses",
                        RaceClass = $"Class: {race.race_class ?? 0}",
                        RaceTime = race.race_time,
                        RaceUrl = $"https://www.racingpost.com/{race.race_url}",
                        Stalls = $"Stalls: {race.Stalls?.stalls_type}",
                        Weather = $"Weather: {race.Weather?.weather_type}",
                        Horses = BuildRaceHorseViewModel(race.RaceHorses)
                    };

                    result.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<RaceHorseViewModel> BuildRaceHorseViewModel(List<RaceHorseEntity> raceHorses) 
        {
            var result = new List<RaceHorseViewModel>();

            foreach (var raceHorse in raceHorses) 
            {
                result.Add(new RaceHorseViewModel()
                {
                    RaceHorseId = raceHorse.race_horse_id,
                    Name = raceHorse.Horse.horse_name,
                    HorseId = raceHorse.horse_id,
                    Age = raceHorse.age,
                    Description = raceHorse.description,
                    Position = raceHorse.position,
                    RaceId = raceHorse.race_id,
                    Weight = raceHorse.weight,
                    JockeyName = raceHorse.Jockey?.jockey_name,
                    TrainerName = raceHorse.Trainer?.trainer_name,
                    Ts = raceHorse.Horse.top_speed,
                    RPR = raceHorse.Horse.rpr,
                });
            }

            return result;
        }
    }
}
