using Core.Entities;

namespace TestHelpers
{
    public class RaceHorseHistoryGenerator
    {
        public static List<RaceHorseEntity> RaceHistoryEmpty()
        {
            return new List<RaceHorseEntity>();
        }

        public static List<RaceHorseEntity> RaceHistoryInexperienced(int horseId, int raceId)
        {
            return new List<RaceHorseEntity> 
            {
                new RaceHorseEntity()
                {
                    race_horse_id = horseId,
                    horse_id = horseId,
                    race_id = raceId,
                    Race = new RaceEntity()
                    {
                    race_id = raceId,
                    description = "",
                    distance = 1,
                    ages = 1,
                    completed = true,
                    event_id = 1,
                    Event = EventEntityGenerator.GetPastEvent( -10),
                    going = 1,
                    no_of_horses = 8
                    },
                    jockey_id = 1,
                    trainer_id = 1,
                    description = "",
                    age = 4,
                    finished = true,
                    position = 4,
                    weight = ""
                }
            };
        }

        public static List<RaceHorseEntity> RaceHistoryExperiencedBad(int horseId)
        {
            return new List<RaceHorseEntity>
            {
                new RaceHorseEntity()
                {
                    race_horse_id = horseId,
                    horse_id = horseId,
                    race_id = 1,
                    Race = new RaceEntity()
                    {
                    race_id = 1,
                    description = "",
                    distance = 1,
                    ages = 1,
                    completed = true,
                    event_id = 1,
                    Event = EventEntityGenerator.GetPastEvent(-10),
                    going = 1,
                    no_of_horses = 8,
                    race_class = 1,
                    stalls = 1,

                    },
                    jockey_id = 1,
                    trainer_id = 1,
                    description = "",
                    age = 4,
                    finished = true,
                    position = 4,
                    weight = ""
                },
                new RaceHorseEntity()
                {
                    race_horse_id = horseId,
                    horse_id = horseId,
                    race_id = 2,
                    Race = new RaceEntity()
                    {
                        race_id = 2,
                        race_class = 1,
                        description = "",
                        distance = 1,
                        ages = 1,
                        completed = true,
                        event_id = 1,
                        Event = EventEntityGenerator.GetPastEvent(-20),
                        going = 1,
                        no_of_horses = 8
                    },
                    jockey_id = 1,
                    trainer_id = 1,
                    description = "",
                    age = 4,
                    finished = true,
                    position = 12,
                    weight = ""
                },
            };
        }
    }
}
