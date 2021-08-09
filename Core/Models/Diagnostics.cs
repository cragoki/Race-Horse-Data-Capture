using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Diagnostics
    {
        public AutomatorEnum Automator { get; set; }
        public DateTime TimeInitialized { get; set; }
        public DateTime TimeCompleted { get; set; }
        public int RacesFiltered { get; set; }
        public int ErrorsEncountered { get; set; }
        public double EllapsedTime { get; set; }
    }
}
