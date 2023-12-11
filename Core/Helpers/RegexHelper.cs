using System.Text.RegularExpressions;

namespace Core.Helpers
{
    public class RegexHelper
    {

        public static string RemoveWhitespace(string word)
        {
            return Regex.Replace(word, @"\s+", "");
        }
    }
}
