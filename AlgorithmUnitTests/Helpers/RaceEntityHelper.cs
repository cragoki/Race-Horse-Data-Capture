using Core.Entities;
using System.Collections.Generic;

namespace AlgorithmUnitTests.Helpers
{
    public static class RaceEntityHelper
    {
        public static RaceEntity BuildSimpleRace(int numberOfHorses)
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
                no_of_horses = numberOfHorses,
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

        public static RaceEntity BuildSimpleSamePastRace(RaceEntity race)
        {
            return new RaceEntity()
            {
                race_id = race.race_id + 1,
                race_time = "",
                description = "",
                completed = true,
                race_url = "",
                rp_race_id = race.rp_race_id + 1,
                stalls = null,
                //Variables
                race_class = 0,
                no_of_horses = race.no_of_horses,
                //Foreign Keys
                distance = race.distance,
                ages = race.ages,
                going = race.going,
                event_id = race.event_id,
                weather = race.weather,
                Weather = MappingEntityHelper.GetWeather(),
                Distance = MappingEntityHelper.GetDistance(),
                Ages = MappingEntityHelper.GetAgeType(),
                Going = MappingEntityHelper.GetGoingType(),
                Event = EventEntityHelper.GenerateSamePastEvent(race),
                RaceHorses = new List<RaceHorseEntity>()
            };
        }
    }
}
