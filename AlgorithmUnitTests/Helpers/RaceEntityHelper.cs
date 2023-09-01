using Core.Entities;
using System.Collections.Generic;

namespace AlgorithmUnitTests.Helpers
{
    public static class RaceEntityHelper
    {
        public static RaceEntity BuildSimpleRace()
        {
            return new RaceEntity()
            {
                race_id = 1,
                race_time = "",
                description = "",
                completed = true,
                race_url = "",
                rp_race_id = 1,
                stalls = null,
                //Variables
                race_class = 0,
                no_of_horses = 2,
                //Foreign Keys
                distance = 1,
                ages = 1,
                going = 1,
                event_id = 1,
                weather = 1,
                Weather = MappingEntityHelper.GetWeather(),
                Distance = MappingEntityHelper.GetDistance(),
                Ages = MappingEntityHelper.GetAgeType(),
                Going = MappingEntityHelper.GetGoingType(),
                Event = EventEntityHelper.GenerateEvent(),
                RaceHorses = new List<RaceHorseEntity>()
            };
        }
    }
}
