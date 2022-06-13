using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class HorseArchiveEntity
    {
        [Key]
        public int archive_id { get; set; }
        public int horse_id { get; set; }
        [ForeignKey("horse_id")]
        public HorseEntity Horse { get; set; }
        public string field_changed { get; set; }
        public string old_value { get; set; }
        public string new_value { get; set; }
        public DateTime date { get; set; }
    }
}
