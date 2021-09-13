using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class RaceHorseEntity
    {
        [Key]
        public int race_horse_id { get; set; }
        public int race_id { get; set; }
        public int horse_id { get; set; }
        public string weight { get; set; }
        public int age { get; set; }
        public int trainer_id { get; set; }
        public int jockey_id { get; set; }
        public string jockey_weight { get; set; }
        public bool finished { get; set; }
        public int position { get; set; }
        public string description { get; set; }
        public string rp_notes { get; set; }
    }
}
