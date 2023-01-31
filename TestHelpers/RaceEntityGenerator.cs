using Core.Entities;
using Core.Variables;
using System.Security.Cryptography.X509Certificates;

namespace TestHelpers
{
    public class RaceEntityGenerator
    {
        public static RaceEntity GenerateCurrentRaceEntity(EventEntity eventEntity, List<RaceHorseEntity> raceHorses)
        {
            return new RaceEntity()
            {
                race_id = 1,
                event_id = 1,
                Event = eventEntity,
                race_time = "",
                rp_race_id = 100,
                weather = 1,
                Weather = new WeatherType()
                {
                    weather_type_id = 1,
                    weather_type = "(Cloudy)"
                },
                no_of_horses = 5,
                going = 1,
                Going = new GoingType()
                {
                    going_type_id = 1,
                    going_type = "Good"
                },
                stalls = 1,
                Stalls = new StallsType()
                {
                    stalls_type_id = 1,
                    stalls_type = "(STALLS Inside)"
                },
                distance = 1,
                Distance = new DistanceType()
                {
                    distance_type_id = 1,
                    distance_type = "3m2f34y"
                },
                race_class = 1,
                ages = 1,
                Ages = new AgeType()
                {
                    age_type_id = 1,
                    age_type = "(4yo+80-95)"
                },
                description = "",
                race_url = "",
                completed = false,
                RaceHorses = raceHorses
            };
        }
    }
}