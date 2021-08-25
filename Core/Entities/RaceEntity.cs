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
        public string weather { get; set; }
        public int no_of_horses { get; set; }
        public string going { get; set; }
        public string stalls { get; set; }
        public string distance { get; set; }
        public int race_class { get; set; }
        public string ages { get; set; }
        public string description { get; set; }
        public string race_url { get; set; }

    }
}
