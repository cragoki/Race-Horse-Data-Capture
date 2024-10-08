﻿using System;
using System.Linq;

namespace Core.Helpers
{
    public static class Extractor
    {
        public static int ExtractIntsFromString(string s)
        {
            var arr = new string(s.SkipWhile(c => !char.IsDigit(c))
             .TakeWhile(c => char.IsDigit(c))
             .ToArray());

            return Int32.Parse(arr);
        }
    }
}
