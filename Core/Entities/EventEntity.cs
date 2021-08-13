using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class EventEntity
    {
        [Key]
        public int event_id { get; set; }
        public int course_id { get; set; }
        public bool? abandoned { get; set; }
        public string surface_type { get; set; }
        public string name { get; set; }
        public string meeting_url { get; set; }
        public string hash_name { get; set; }
        public string meeting_type { get; set; }
        public int races { get; set; }
        public Guid batch_id { get; set; }
        public DateTime created { get; set; }

    }
}
