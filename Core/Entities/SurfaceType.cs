using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class SurfaceType
    {
        [Key]
        public int surface_type_id { get; set; }
        public string surface_type { get; set; }
    }
}
