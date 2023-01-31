using Core.Entities;

namespace TestHelpers
{
    public class EventGenerator
    {
        public static IQueryable<RaceEntity> GetEvents()
        {
            var result = new List<RaceEntity>();
            result.Add(GetEasyTestCaseLastTwo());

            return result.AsQueryable();
        }

        public static RaceEntity GetEasyTestCaseLastTwo()
        {
            return new RaceEntity()
            {
                race_id = 27608,
                event_id = 8812,
                Event = new EventEntity()
                {
                    event_id = 8812,
                    course_id = 57,
                    Course = new CourseEntity()
                    {
                        course_id = 57,
                        rp_course_id = 57,
                        name = "Sedgefield",
                        country_code = "GB",
                        all_weather = false,
                        course_url = "/profile/course/57/sedgefield"
                    },
                    surface_type = null,
                    name = "Sedgefield_01/29/2023",
                    meeting_url = "/racecards/57/sedgefield/2023-01-29",
                    hash_name = "sedgefield",
                    meeting_type = 2,
                    MeetingType = new MeetingType()
                    {
                        meeting_type_id = 2,
                        meeting_type = "Jumps"
                    },
                    races = 7,
                    batch_id = new Guid(),
                    created = DateTime.Now,
                },
                race_time = "14:35",
                rp_race_id = 829587,
                weather = 6,
                Weather = new WeatherType()
                {
                    weather_type_id = 6,
                    weather_type = "(Mostly cloudy)"
                },
                no_of_horses = 6,
                going = 10,
                Going = new GoingType()
                {
                    going_type_id = 10,
                    going_type = "Good To Soft"
                },
                stalls = 6,
                Stalls = new StallsType()
                {
                    stalls_type_id = 6,
                    stalls_type = ""
                },
                distance = 51,
                Distance = new DistanceType()
                {
                    distance_type_id = 51,
                    distance_type = "2m1f"
                },
                race_class = 4,
                ages = 161,
                Ages = new AgeType()
                { 
                    age_type_id = 161,
                    age_type = "(5yo+0-120)"
                },
                description = "",
                race_url = "/racecards/57/sedgefield/2023-01-29/829587",
                completed = true,
                RaceHorses = new List<RaceHorseEntity>() 
                {
                    new RaceHorseEntity()
                    {
                        race_horse_id = 220838,
                        race_id = 27608,
                        horse_id = 6091,
                        Horse = new HorseEntity()
                        {
                            horse_id = 6091,
                            Races =  new List<RaceHorseEntity>()
                            {
                                new RaceHorseEntity()
                                {
                                    race_horse_id = 209918,
                                    race_id = 26568,
                                    Race = new RaceEntity()
                                    {
                                        race_id = 8638,
                                        race_class = 3,
                                        distance = 444,
                                        Distance = new DistanceType()
                                        {
                                            distance_type_id = 444,
                                            distance_type = "2m78y"
                                        },
                                        event_id = 8638,
                                        Event = new EventEntity()
                                        {
                                            event_id = 8638,
                                            created = new DateTime(2022,12,29)
                                        }
                                    },
                                    horse_id = 6091,
                                    weight = "10.11",
                                    age = 7,
                                    trainer_id = 186,
                                    jockey_id = 412,
                                    finished = true,
                                    position = 3
                                },
                                new RaceHorseEntity()
                                {
                                    race_horse_id = 202127,
                                    race_id = 25873,
                                    Race = new RaceEntity()
                                    {
                                        race_id = 25873,
                                        race_class = 3,
                                        distance = 28,
                                        Distance = new DistanceType()
                                        {
                                            distance_type_id = 444,
                                            distance_type = "2m75y"
                                        },
                                        event_id = 8511,
                                        Event = new EventEntity()
                                        {
                                            event_id = 8511,
                                            created = new DateTime(2022,12,08)
                                        }
                                    },
                                    horse_id = 6091,
                                    weight = "11.6",
                                    age = 7,
                                    trainer_id = 186,
                                    jockey_id = 401,
                                    finished = true,
                                    position = 3
                                },
                            }
                        },
                        weight = "12.2",
                        age = 8,
                        trainer_id = 186,
                        jockey_id = 5399,
                        finished = true,
                        position = 5
                    },
                    new RaceHorseEntity()
                    {
                        race_horse_id = 220839,
                        race_id = 27608,
                        horse_id = 25896,
                        Horse = new HorseEntity()
                        {
                            horse_id = 25896,
                            Races =  new List<RaceHorseEntity>()
                            {
                                new RaceHorseEntity()
                                {
                                    race_horse_id = 185615,
                                    race_id = 24337,
                                    Race = new RaceEntity()
                                    {
                                        race_id = 24337,
                                        race_class = 3,
                                        distance = 88,
                                        Distance = new DistanceType()
                                        {
                                            distance_type_id = 88,
                                            distance_type = "2m4f"
                                        },
                                        event_id = 8262,
                                        Event = new EventEntity()
                                        {
                                            event_id = 8262,
                                            created = new DateTime(2022,10,30)
                                        }
                                    },
                                    horse_id = 25896,
                                    weight = "11.6",
                                    age = 6,
                                    trainer_id = 474,
                                    jockey_id = 411,
                                    finished = true,
                                    position = 7
                                },
                                new RaceHorseEntity()
                                {
                                    race_horse_id = 86429,
                                    race_id = 14704,
                                    Race = new RaceEntity()
                                    {
                                        race_id = 14704,
                                        race_class = 3,
                                        distance = 204,
                                        Distance = new DistanceType()
                                        {
                                            distance_type_id = 204,
                                            distance_type = "2m4f100y"
                                        },
                                        event_id = 6680,
                                        Event = new EventEntity()
                                        {
                                            event_id = 6680,
                                            created = new DateTime(2022,04,01)
                                        }
                                    },
                                    horse_id = 25896,
                                    weight = "11.12",
                                    age = 6,
                                    trainer_id = 474,
                                    jockey_id = 411,
                                    finished = true,
                                    position = 2
                                },
                            }
                        },
                        weight = "12.2",
                        age = 7,
                        trainer_id = 474,
                        jockey_id = 411,
                        finished = true,
                        position = 4
                    },
                    new RaceHorseEntity()
                    {
                        race_horse_id = 220840,
                        race_id = 27608,
                        horse_id = 9509,
                        Horse = new HorseEntity()
                        {
                            horse_id = 9509,
                            Races =  new List<RaceHorseEntity>()
                            {

                            }
                        },
                        weight = "12.0",
                        age = 9,
                        trainer_id = 217,
                        jockey_id = 407,
                        finished = true,
                        position = 1

                    },
                    new RaceHorseEntity()
                    {
                        race_horse_id = 220841,
                        race_id = 27608,
                        horse_id = 15085,
                        Horse = new HorseEntity()
                        {
                            horse_id = 15085,
                            Races =  new List<RaceHorseEntity>()
                            {

                            }
                        },
                        weight = "11.4",
                        age = 10,
                        trainer_id = 7538,
                        jockey_id = 163,
                        finished = false,
                        position = 0
                    },
                    new RaceHorseEntity()
                    {
                        race_horse_id = 220842,
                        race_id = 27608,
                        horse_id = 13268,
                        Horse = new HorseEntity()
                        {
                            horse_id = 13268,
                            Races =  new List<RaceHorseEntity>()
                            {

                            }
                        },
                        weight = "11.0",
                        age = 8,
                        trainer_id = 497,
                        jockey_id = 401,
                        finished = true,
                        position = 2
                    },
                    new RaceHorseEntity()
                    {
                        race_horse_id = 220843,
                        race_id = 27608,
                        horse_id = 27241,
                        Horse = new HorseEntity()
                        {
                            horse_id = 13268,
                            Races =  new List<RaceHorseEntity>()
                            {

                            }
                        },
                        weight = "10.6",
                        age = 6,
                        trainer_id = 7621,
                        jockey_id = 6405,
                        finished = true,
                        position = 3
                    }
                }
            };

        }
    }
}
