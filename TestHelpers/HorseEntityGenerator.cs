using Core.Entities;

namespace TestHelpers
{
    public class HorseEntityGenerator
    {

        public static HorseEntity GenerateHorseBasic(int horseId, List<RaceHorseEntity> races)
        {
            return new HorseEntity()
            {
                horse_id = horseId,
                rp_horse_id = 100,
                horse_name = $"Horse No {horseId}",
                dob = DateTime.Now,
                horse_url = "",
                top_speed = 90,
                rpr = 10,
                Races = races
            };
        }
    }
}
