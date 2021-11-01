using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AlgorithmEntity
    {
        [Key]
        public int algorithm_id { get; set; }
        public string algorithm_name { get; set; }
        public bool active { get; set; }
        public int accuracy { get; set; }
    }
}
