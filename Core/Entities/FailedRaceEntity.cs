using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class FailedRaceEntity
    {
        [Key]
        public int failed_race_id { get; set; }
        public int race_id { get; set; }
        [ForeignKey("race_id")]
        public RaceEntity Race { get; set; }
        public string error_message { get; set; }
        public int attempts { get; set; }

    }
}
