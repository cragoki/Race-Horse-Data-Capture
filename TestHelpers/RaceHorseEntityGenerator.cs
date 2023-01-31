using Core.Entities;

namespace TestHelpers
{
    public class RaceHorseEntityGenerator
    {

        public static RaceHorseEntity GenerateBasicCurrentRaceHorseEntityNoHistory(int horseId)
        {
            return new RaceHorseEntity()
            {
                race_horse_id = horseId,
                race_id = 1,
                horse_id = horseId,
                Horse = HorseEntityGenerator.GenerateHorseBasic(horseId, RaceHorseHistoryGenerator.RaceHistoryEmpty()),
                weight = "10. 0",
                age = 4,
                trainer_id = 1,
                Trainer = new TrainerEntity()
                {
                    trainer_id = 1,
                    trainer_name = "Test Trainer",
                    trainer_url = ""
                },
                jockey_id = 1,
                Jockey = new JockeyEntity()
                {
                    jockey_id = 1,
                    jockey_name = "Test Jockey",
                    jockey_url = ""
                },
                finished = false,
                position = 0,
                description = "",
                rp_notes = ""
            };           
        }

        public static RaceHorseEntity GenerateBasicCurrentRaceHorseEntityNoHistory(int horseId, int raceId)
        {
            return new RaceHorseEntity()
            {
                race_horse_id = horseId,
                race_id = 1,
                horse_id = horseId,
                Horse = HorseEntityGenerator.GenerateHorseBasic(horseId, RaceHorseHistoryGenerator.RaceHistoryExperiencedBad(horseId, raceId)),
                weight = "10. 0",
                age = 4,
                trainer_id = 1,
                Trainer = new TrainerEntity()
                {
                    trainer_id = 1,
                    trainer_name = "Test Trainer",
                    trainer_url = ""
                },
                jockey_id = 1,
                Jockey = new JockeyEntity()
                {
                    jockey_id = 1,
                    jockey_name = "Test Jockey",
                    jockey_url = ""
                },
                finished = false,
                position = 0,
                description = "",
                rp_notes = ""
            };
        }
    }
}
