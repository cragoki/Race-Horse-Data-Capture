using Core.Entities;

namespace AlgorithmUnitTests.Helpers
{
    public static class MappingEntityHelper
    {
        public static WeatherType GetWeather() 
        {
            return new WeatherType()
            {
                weather_type_id = 1,
                weather_type = "Rain"
            };
        }

        public static DistanceType GetDistance() 
        {
            return new DistanceType()
            {
                distance_type_id = 1,
                distance_type = "1f"
            };
        }

        public static AgeType GetAgeType() 
        {
            return new AgeType()
            {
                age_type_id = 1,
                age_type = "2yo"
            };
        }

        public static GoingType GetGoingType() 
        {
            return new GoingType()
            {
                going_type_id = 1,
                going_type = "Good"
            };
        }

        public static MeetingType GetMeetingType()
        {
            return new MeetingType()
            {
                meeting_type_id = 1,
                meeting_type = "Flat"
            };
        }

        public static CourseEntity GetCourse()
        {
            return new CourseEntity()
            {
                course_id = 1,
                name = "Ascot",
                all_weather = false
            };
        }

        public static SurfaceType GetSurface()
        {
            return new SurfaceType()
            {
                surface_type_id = 1,
                surface_type = "Polytrack"
            };
        }
    }
}
