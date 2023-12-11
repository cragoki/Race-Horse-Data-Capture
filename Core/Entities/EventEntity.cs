using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class EventEntity
    {
        [Key]
        public int event_id { get; set; }
        public int course_id { get; set; }
        [ForeignKey("course_id")]
        public CourseEntity Course { get; set; }
        public bool? abandoned { get; set; }
        public int? surface_type { get; set; }
        [ForeignKey("surface_type")]
        public SurfaceType Surface { get; set; }
        public string name { get; set; }
        public string meeting_url { get; set; }
        public string hash_name { get; set; }
        public int? meeting_type { get; set; }
        [ForeignKey("meeting_type")]
        public MeetingType MeetingType { get; set; }
        public int races { get; set; }
        public Guid batch_id { get; set; }
        public DateTime created { get; set; }
        public List<RaceEntity> Races { get; set; }
    }
}
