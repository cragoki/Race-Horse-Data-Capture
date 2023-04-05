using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public static class FormatHelper
    {
        public static decimal ToTwoPlaces(decimal value) 
        {
            return decimal.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}
