using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class HorseArchiveEntity
    {
        [Key]
        public int archive_id { get; set; }
        public int horse_id { get; set; }
        public string field_changed { get; set; }
        public string old_value { get; set; }
        public string new_value { get; set; }
        public DateTime date { get; set; }
    }
}
