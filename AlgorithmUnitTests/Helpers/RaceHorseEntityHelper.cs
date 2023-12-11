using Core.Entities;
using Core.Helpers;
using System;
using System.Collections.Generic;

namespace AlgorithmUnitTests.Helpers
{
    public static class RaceHorseEntityHelper
    {
        public static RaceHorseEntity GenerateRaceHorse(int index, int races, int wins, int places, RaceEntity race)
        {
            return new RaceHorseEntity()
            {
                race_horse_id = index,
                description = "",
                finished = false,
                age = 2,
                horse_id = index,
                jockey_id = index,
                trainer_id = index,
                weight = " 10. 12",
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
                    Races = GenerateRaceHorseHistory(races, wins, places, race, index)
                },
                race_id = race.race_id,
                Race = new RaceEntity()
                {
                    race_id = 1000 + index,
                    event_id = 1000 + index,
                    Event = new EventEntity()
                    {
                        course_id = race.Event.course_id
                    },
                    race_class = race.race_class
                }
            };
        }

        //Generate Race Horse History
        public static List<RaceHorseEntity> GenerateRaceHorseHistory(int noOfRaces, int wins, int places, RaceEntity race, int index)
        {
            var result = new List<RaceHorseEntity>();
            int currentWins = 0;
            int currentPlaces = 0;
            var place = SharedCalculations.GetTake(race.no_of_horses ?? 0);
            //Need to build a RaceHorseEntity with
            // 1. A Race.Event entity where x.Race.Event.created < race.Event.created
            //BuildSimpleSamePastRace(race)
            // 2. A Race.race_id which is different to the current race
            // 3. Race.Event.course_id which is different or the same as current
            // 4. Race.distance which is different or the same as current
            // 5. Race.Event.meeting_type which is different or the same as current
            // 6. Race.going which is different or the same as current
            for (int i = 0; i <= noOfRaces; i++)
            {
                if (currentWins < wins)
                {
                    // Add a win
                    result.Add(new RaceHorseEntity()
                    {
                        race_horse_id = 1,
                        race_id = race.race_id + 1,
                        Race = RaceEntityHelper.BuildSimpleSamePastRace(race),
                        position = 1,
                        horse_id = index,
                        jockey_id = index,
                        trainer_id = index,
                        weight = " 10. 12"
                    });
                    currentWins++;
                    continue;
                }
                if (currentPlaces < places)
                {
                    // Add a place TODO GET TAKE
                    result.Add(new RaceHorseEntity()
                    {
                        race_horse_id = 1,
                        race_id = race.race_id + 1,
                        Race = RaceEntityHelper.BuildSimpleSamePastRace(race),
                        position = place,
                        horse_id = index,
                        jockey_id = index,
                        trainer_id = index,
                        weight = " 10. 12"
                    });

                    currentPlaces++;
                    continue;
                }

                //Add a loss             
                result.Add(new RaceHorseEntity()
                {
                    race_horse_id = 1,
                    race_id = race.race_id + 1,
                    Race = RaceEntityHelper.BuildSimpleSamePastRace(race),
                    position = race.no_of_horses ?? 0,
                    horse_id = index,
                    jockey_id = index,
                    trainer_id = index,
                    weight = " 10. 12"
                });
            }
            return result;
        }

        public static RaceHorseEntity GenerateJockeyHistory(int index, int position)
        {
            return new RaceHorseEntity()
            {
                race_horse_id = index,
                description = "",
                position = position,
                finished = true,
                age = 2,
                jockey_id = index,
                trainer_id = index,
                horse_id = index,
                weight = " 10. 12",
                Horse = new HorseEntity()
                {
                    horse_id = index,
                    horse_name = $"Horse {index}",
                    rp_horse_id = index,
                    Archive = null,
                    dob = null,
                    horse_url = null,
                    rpr = null,
                    top_speed = null
                },
                race_id = 1000 + index,
                Race = new RaceEntity()
                {
                    race_id = 1000 + index,
                    event_id = 1000 + index,
                    Event = new EventEntity()
                    {
                        created = DateTime.Now.AddMonths(-3)
                    }
                }
            };
        }
    }
}
