using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class RaceEntity
    {
        [Key]
        public int race_id { get; set; }
        public int event_id { get; set; }
        public string race_time { get; set; }
        public int rp_race_id { get; set; }
        public int? weather { get; set; }
        public int? no_of_horses { get; set; }
        public int? going { get; set; }
        public int? stalls { get; set; }
        public int? distance { get; set; }
        public int? race_class { get; set; }
        public int? ages { get; set; }
        public string description { get; set; }
        public string race_url { get; set; }
        public bool completed { get; set; }

    }
}
