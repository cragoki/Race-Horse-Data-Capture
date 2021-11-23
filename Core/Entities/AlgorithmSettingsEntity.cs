
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AlgorithmSettingsEntity
    {
        [Key]
        public int algorithm_setting_id { get; set; }
        public int algorithm_id { get; set; }
        public string setting_name { get; set; }
        public string setting_value { get; set; }
    }
}
