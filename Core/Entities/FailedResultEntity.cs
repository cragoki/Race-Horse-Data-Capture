using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class FailedResultEntity
    {
        [Key]
        public int failed_result_id { get; set; }
        public int race_horse_id { get; set; }
        [ForeignKey("race_horse_id")]
        public RaceHorseEntity RaceHorse { get; set; }
        public string error_message { get; set; }
    }
}
