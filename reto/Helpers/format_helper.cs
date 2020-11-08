using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace reto.Helpers
{
    static class format_helper
    {
        public static string getNumberYahoo(string responseString)
        {
            Regex pRegex = new Regex("<div class=\"compPagination\".*?>(.*?)results</span>", RegexOptions.IgnoreCase);
            string result = pRegex.Match(responseString).Value;
            Regex pRegexSpan = new Regex("<span.*?(?=</span>)", RegexOptions.IgnoreCase);
            result = pRegexSpan.Match(result).Value;
            result = Regex.Replace(result, "<.*?>", String.Empty);
            string stripped = Regex.Replace(result, "[^0-9]", "");
            return string.IsNullOrEmpty(stripped) ? "0" : stripped;
        }
        public static string getNumberBing(string responseString)
        {
            Regex pRegex = new Regex("<span class=\"sb_count\".*?>(.*?)</span>", RegexOptions.IgnoreCase);
            string result = pRegex.Match(responseString).Value;
            result = Regex.Replace(result, "<.*?>", String.Empty);
            string stripped = Regex.Replace(result, "[^0-9]", "");
            return string.IsNullOrEmpty(stripped) ? "0" :stripped;            
        }
        public static string getNumberGoogle(string responseString)
        {
            dynamic json = JsonConvert.DeserializeObject(responseString);
            return (json.searchInformation.totalResults);
        }
        
    }
}
