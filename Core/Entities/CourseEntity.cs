using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class CourseEntity
    {
        [Key]
        public int course_id { get; set; }
        public int? rp_course_id { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public bool all_weather { get; set; }
        public string course_url { get; set; }
    }
}
