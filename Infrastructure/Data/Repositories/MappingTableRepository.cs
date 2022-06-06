using Core.Entities;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class MappingTableRepository : IMappingTableRepository
    {
        private readonly DbContextData _context;
        private readonly IConfigurationService _configService;

        public MappingTableRepository(DbContextData context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService;
        }

        public string GetAgeType(int age)
        {
            return _context.tb_age_type.Where(x => x.age_type_id == age).FirstOrDefault().age_type;
        }
        public string GetDistanceType(int distance)
        {
            return _context.tb_distance_type.Where(x => x.distance_type_id == distance).FirstOrDefault().distance_type;
        }
        public string GetGoingType(int going)
        {
            return _context.tb_going_type.Where(x => x.going_type_id == going).FirstOrDefault().going_type;
        }
        public string GetMeetingType(int meeting)
        {
            return _context.tb_meeting_type.Where(x => x.meeting_type_id == meeting).FirstOrDefault().meeting_type;
        }
        public string GetStallsType(int stalls)
        {
            return _context.tb_stalls_type.Where(x => x.stalls_type_id == stalls).FirstOrDefault().stalls_type;
        }
        public string GetWeatherType(int weather)
        {
            return _context.tb_weather_type.Where(x => x.weather_type_id == weather).FirstOrDefault().weather_type;
        }
        public string GetSurfaceType(int surface)
        {
            return _context.tb_surface_type.Where(x => x.surface_type_id == surface).FirstOrDefault().surface_type;
        }

        public int AddOrReturnAgeType(string age)
        {
            int result = 0;

            var existing = _context.tb_age_type.Where(x => x.age_type == age).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new AgeType()
                {
                    age_type = age
                };
                _context.tb_age_type.Add(toAdd);
                SaveChanges();

                result = toAdd.age_type_id;
            }
            else
            {
                result = existing.age_type_id;
            }

            return result;
        }

        public int AddOrReturnDistanceType(string distance)
        {
            int result = 0;

            var existing = _context.tb_distance_type.Where(x => x.distance_type == distance).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new DistanceType()
                {
                    distance_type = distance
                };
                _context.tb_distance_type.Add(toAdd);
                SaveChanges();

                result = toAdd.distance_type_id;
            }
            else
            {
                result = existing.distance_type_id;
            }

            return result;
        }

        public int AddOrReturnGoingType(string going)
        {
            int result = 0;

            var existing = _context.tb_going_type.Where(x => x.going_type == going).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new GoingType()
                {
                    going_type = going
                };
                _context.tb_going_type.Add(toAdd);
                SaveChanges();

                result = toAdd.going_type_id;
            }
            else
            {
                result = existing.going_type_id;
            }

            return result;
        }

        public int AddOrReturnMeetingType(string meeting)
        {
            int result = 0;

            var existing = _context.tb_meeting_type.Where(x => x.meeting_type == meeting).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new MeetingType()
                {
                    meeting_type = meeting
                };
                _context.tb_meeting_type.Add(toAdd);
                SaveChanges();

                result = toAdd.meeting_type_id;
            }
            else
            {
                result = existing.meeting_type_id;
            }

            return result;
        }

        public int AddOrReturnStallsType(string stalls)
        {
            int result = 0;

            var existing = _context.tb_stalls_type.Where(x => x.stalls_type == stalls).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new StallsType()
                {
                    stalls_type = stalls
                };
                _context.tb_stalls_type.Add(toAdd);
                SaveChanges();

                result = toAdd.stalls_type_id;
            }
            else
            {
                result = existing.stalls_type_id;
            }

            return result;
        }

        public int AddOrReturnSurfaceType(string surface)
        {
            int result = 0;

            var existing = _context.tb_surface_type.Where(x => x.surface_type == surface).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new SurfaceType()
                {
                    surface_type = surface
                };
                _context.tb_surface_type.Add(toAdd);
                SaveChanges();

                result = toAdd.surface_type_id;
            }
            else
            {
                result = existing.surface_type_id;
            }

            return result;
        }

        public int AddOrReturnWeatherType(string weather)
        {
            int result = 0;

            var existing = _context.tb_weather_type.Where(x => x.weather_type == weather).FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new WeatherType()
                {
                    weather_type = weather
                };
                _context.tb_weather_type.Add(toAdd);
                SaveChanges();

                result = toAdd.weather_type_id;
            }
            else
            {
                result = existing.weather_type_id;
            }

            return result;
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
