using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public class SharedCalculations
    {
        public static int GetTake(int noOfHorses)
        {
            return 
            Enumerable.Range(1, 4).Contains(noOfHorses) ? 1 :
            Enumerable.Range(5, 7).Contains(noOfHorses) ? 2 :
            Enumerable.Range(8, 15).Contains(noOfHorses) ? 3 : 4;
        }
    }
}
