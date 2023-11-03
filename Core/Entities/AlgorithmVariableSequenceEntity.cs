using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AlgorithmVariableSequenceEntity
    {
        [Key]
        public int algorithm_variable_sequence_id { get; set; }
        public Guid batch_id { get; set; }
        public int no_of_races { get; set; }
        public decimal percentage_correct { get; set; }
        public bool is_first_in_sequence { get; set; }
        public DateTime date { get; set; }
    }
}
