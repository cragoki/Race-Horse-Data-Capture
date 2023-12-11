using System;

namespace Core.Helpers
{
    public static class TestCaseHelper
    {

        public static string NewEntity(string entityName)
        {
            return "new " + entityName + "() {" + (Environment.NewLine);
        }

        public static string CloseEntity(string p)
        {
            return "}" + p + (Environment.NewLine);
        }

        public static string FormatString(string val)
        {
            return $"\"{val.Replace("'", "").Replace("\n", "")}\"";
        }
    }
}
