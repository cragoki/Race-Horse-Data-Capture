using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class AlgorithmVariableEntity
    {
        public int algorithm_variable_id { get; set; }
        public int algorithm_id { get; set; }
        public int variable_id { get; set; }
        public decimal threshold { get; set; }
    }
}
