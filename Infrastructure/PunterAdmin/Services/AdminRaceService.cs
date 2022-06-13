
using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
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
        private static IRaceService _raceService;

        public AdminRaceService(IConfigurationRepository configRepo, IEventRepository eventRepository, IMappingTableRepository mappingRepository, IHorseRepository horseRepository, IRaceService raceService)
        {
            _configRepo = configRepo;
            _eventRepository = eventRepository;
            _mappingRepository = mappingRepository;
            _horseRepository = horseRepository;
            _raceService = raceService;
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
                        EventRaces =  BuildTodaysRaceViewModel(ev.Races, ev.created), 
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

        public async Task<RaceHorseStatisticsViewModel> GetHorseStatistics(RaceHorseViewModel raceHorse, RaceViewModel race)
        {
            var result = new RaceHorseStatisticsViewModel();

            try
            {
                result.HorseId = raceHorse.HorseId;
                result.Age = raceHorse.Age;
                result.HorseName = raceHorse.Name;
                result.RPR = raceHorse.RPR;
                result.TS = raceHorse.Ts;
                result.Weight = raceHorse.Weight;
                result.CurrentRace = race;
                result.Horse = raceHorse;
                result.HorseRaces = GetRacesForHorse(raceHorse.HorseId);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<RaceViewModel> GetRacesForHorse(int horseId)
        {
            var result = new List<RaceViewModel>();
            try 
            {
                var horse = _horseRepository.GetHorseWithRaces(horseId);

                foreach (var race in horse.Races)
                {
                    result.Add(new RaceViewModel()
                    {
                        RaceId = race.race_id,
                        Date = race.Race.Event.created,
                        Ages = race.Race.Ages?.age_type,
                        Completed = race.Race.completed,
                        Description = race.description,
                        Distance = race.Race.Distance?.distance_type,
                        EventId = race.Race.event_id,
                        Going = $"Going: {race.Race.Going?.going_type}",
                        NumberOfHorses = $"{race.Race.no_of_horses} Horses",
                        RaceClass = $"Class: {race.Race.race_class ?? 0}",
                        RaceTime = race.Race.race_time,
                        RaceUrl = $"https://www.racingpost.com/{race.Race.race_url}",
                        Stalls = $"Stalls: {race.Race.Stalls?.stalls_type}",
                        Weather = $"Weather: {race.Race.Weather?.weather_type}",
                        Horses = BuildRaceHorseViewModel(race.Race.RaceHorses, race.Race.Event)
                    });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private List<RaceViewModel> BuildTodaysRaceViewModel(List<RaceEntity> races, DateTime date) 
        {
            var result = new List<RaceViewModel>();

            try
            {
                foreach (var race in races)
                {

                    var toAdd = new RaceViewModel()
                    {
                        RaceId = race.race_id,
                        Date = date,
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
                        Horses = BuildRaceHorseViewModel(race.RaceHorses, race.Event)
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

        private List<RaceHorseViewModel> BuildRaceHorseViewModel(List<RaceHorseEntity> raceHorses, EventEntity even) 
        {
            var result = new List<RaceHorseViewModel>();

            foreach (var raceHorse in raceHorses)
            {
                var rpr = ConfigureRPR(raceHorse, even.created);
                var ts = ConfigureTS(raceHorse, even.created);

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
                    Ts = ts,
                    RPR = rpr,
                });
            }

            return result;
        }

        private int? ConfigureRPR(RaceHorseEntity raceHorse, DateTime created) 
        {
            var result = 0;

            if (raceHorse.Horse.Archive != null && raceHorse.Horse.Archive.Count() != 0)
            {
                var archive = raceHorse.Horse.Archive;
                var rprString = archive.Where(x => x.field_changed == "rpr" && x.date < created)
                    .OrderByDescending(x => x.date).FirstOrDefault()?.new_value;

                if (rprString != "-")
                {
                    if (Int32.TryParse(rprString, out var rprInt))
                    {
                        result = rprInt;
                    }
                }
            }

            return result;
        }

        private int? ConfigureTS(RaceHorseEntity raceHorse, DateTime created)
        {
            var result = 0;

            if (raceHorse.Horse.Archive != null && raceHorse.Horse.Archive.Count() != 0)
            {
                var archive = raceHorse.Horse.Archive;
                var tsString = archive.Where(x => x.field_changed == "ts" && x.date < created)
                    .OrderByDescending(x => x.date).FirstOrDefault()?.new_value;

                if (tsString != "-")
                {
                    if (Int32.TryParse(tsString, out var tsInt))
                    {
                        result = tsInt;
                    }
                }
            }

            return result;
        }
    }
}
