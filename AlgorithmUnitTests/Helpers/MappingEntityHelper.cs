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
    }
}
