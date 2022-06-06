using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Core.Entities
{
    public class HorseEntity
    {
        [Key]
        public int horse_id { get; set; }
        public int rp_horse_id { get; set; }
        public string horse_name { get; set; }
        public DateTime? dob { get; set; }
        public string horse_url { get; set; }
        public int? top_speed { get; set; }
        public int? rpr { get; set; }
        public List<RaceHorseEntity> Races { get; set; }
    }
}
