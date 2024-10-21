using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LinkPreviewApp.Common
{
    public static class DictionaryExtension
    {
        public static string ToQueryString(this Dictionary<string, string> dict)
        {
            if (null == dict || !dict.Any())
            {
                return string.Empty;
            }
            string queryString = string.Join("&", dict
                .Where(kv => kv.Value != null)
                .Select(kv => $"{kv.Key}={kv.Value}"));

            return $"?{queryString}";
        }
    }
}
