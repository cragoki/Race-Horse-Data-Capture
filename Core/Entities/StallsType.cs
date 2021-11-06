using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class StallsType
    {
        [Key]
        public int stalls_type_id { get; set; }
        public string stalls_type { get; set; }
    }
}
