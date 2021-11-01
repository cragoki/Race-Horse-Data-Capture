using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AlgorithmVariableEntity
    {
        [Key]
        public int algorithm_variable_id { get; set; }
        public int algorithm_id { get; set; }
        public int variable_id { get; set; }
        public decimal threshold { get; set; }
    }
}
