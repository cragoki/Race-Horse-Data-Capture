using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class DistanceType
    {
        [Key]
        public int distance_type_id { get; set; }
        public string distance_type { get; set; }
    }
}
