using System;
using System.Linq;

namespace SGAM.Elfec.DataAccess.WebServices.Converters
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Solo funciona si la cadena actual esta en camel
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromCamelToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        /// <summary>
        /// Solo funciona si la cadena actual esta en snake case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromSnakeToCamelCase(this string str)
        {
            return str.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                .Aggregate(string.Empty, (s1, s2) => s1 + s2);
        }
    }
}
