using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class GoingType
    {
        [Key]
        public int going_type_id { get; set; }
        public string going_type { get; set; }
    }
}
