using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class AlgorithmEntity
    {
        public int algorithm_id { get; set; }
        public string algorithm_name { get; set; }
        public bool active { get; set; }
        public int accuracy { get; set; }
    }
}
