using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AlgorithmEntity
    {
        [Key]
        public int algorithm_id { get; set; }
        public string algorithm_name { get; set; }
        public bool active { get; set; }
        public decimal? accuracy { get; set; }
        public int? number_of_races { get; set; }
        public List<AlgorithmSettingsEntity> Settings { get; set; }
        public List<AlgorithmVariableEntity> Variables { get; set; }

    }
}
