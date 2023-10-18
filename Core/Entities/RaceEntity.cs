using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class RaceEntity
    {
        [Key]
        public int race_id { get; set; }
        public int event_id { get; set; }
        [ForeignKey("event_id")]
        public EventEntity Event { get; set; }
        public string race_time { get; set; }
        public int rp_race_id { get; set; }
        public int? weather { get; set; }
        [ForeignKey("weather")]
        public WeatherType Weather { get; set; }
        public int? no_of_horses { get; set; }
        public int? going { get; set; }
        [ForeignKey("going")]
        public GoingType Going { get; set; }
        public int? stalls { get; set; }
        [ForeignKey("stalls")]
        public StallsType Stalls { get; set; }
        public int? distance { get; set; }
        [ForeignKey("distance")]
        public DistanceType Distance { get; set; }
        public int? race_class { get; set; }
        public int? ages { get; set; }
        [ForeignKey("ages")]
        public AgeType Ages { get; set; }
        public string description { get; set; }
        public string race_url { get; set; }
        public bool completed { get; set; }
        public List<RaceHorseEntity> RaceHorses { get; set; }
        public string winning_time { get; set; }


    }
}
