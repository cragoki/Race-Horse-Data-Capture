using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class WeatherType
    {
        [Key]
        public int weather_type_id { get; set; }
        public string weather_type { get; set; }
    }
}
