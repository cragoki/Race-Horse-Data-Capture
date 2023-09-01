
using Core.Entities;
using System.Collections.Generic;

sing Core.Entities;
using System.Collections.Generic;

namespace AlgorithmUnitTests.Helpers
{
    public static class RaceHorseEntityHelper
    {
        public static RaceHorseEntity GenerateRaceHorse(int index, int raceId, int races, int wins, int places, RaceEntity race) 
        {
            return new RaceHorseEntity()
            {
                race_horse_id = index,
                description = "",
                finished = false,
                age = 2,
                horse_id = index,
                Horse = new HorseEntity() 
                {
                    horse_id = index,
                    horse_name = $"Horse {index}",
                    rp_horse_id = index,
                    Archive = null,
                    dob = null,
                    horse_url = null,
                    rpr = null,
                    top_speed = null,
                    Races = GenerateRaceHorseHistory(races, wins, places, race)
                },
                race_id = race.race_id,
            };
        }

        //Generate Race Horse History
        public static List<RaceHorseEntity> GenerateRaceHorseHistory(int noOfRaces, int wins, int places, RaceEntity race)
        {
            var result = new List<RaceHorseEntity>();
            int currentWins = 0;
            int currentPlaces = 0;

            for (int i = 0; i <= noOfRaces; i++) 
            {
                if (currentWins < wins) 
                {
                    // Add a win

                    continue;
                }
                if (currentPlaces < places)
                {
                    // Add a place
                    continue;
                }

                //Add a loss             
            }
            return new List<RaceHorseEntity>();
        }
    }
}
