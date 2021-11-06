using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AgeType
    {
        [Key]
        public int age_type_id { get; set; }
        public string age_type { get; set; }
    }
}
