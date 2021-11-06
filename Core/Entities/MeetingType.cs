using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class MeetingType
    {
        [Key]
        public int meeting_type_id { get; set; }
        public string meeting_type { get; set; }
    }
}
