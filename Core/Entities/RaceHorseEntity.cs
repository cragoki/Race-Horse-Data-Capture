using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class RaceHorseEntity
    {
        [Key]
        public int race_horse_id { get; set; }
        public int race_id { get; set; }
        [ForeignKey("race_id")]
        public RaceEntity Race { get; set; }
        public int horse_id { get; set; }
        [ForeignKey("horse_id")]
        public HorseEntity Horse { get; set; }
        public string weight { get; set; }
        public int age { get; set; }
        public int trainer_id { get; set; }
        [ForeignKey("trainer_id")]
        public TrainerEntity Trainer { get; set; }
        public int jockey_id { get; set; }
        [ForeignKey("jockey_id")]
        public JockeyEntity Jockey { get; set; }
        public bool finished { get; set; }
        public int position { get; set; }
        public string description { get; set; }
        public string rp_notes { get; set; }
    }
}
