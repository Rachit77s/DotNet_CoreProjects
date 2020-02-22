using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WMIConnection
{
    // //M-5   Extension methods
    public static class StringExtender
    {
        public static int NthIndexOf(this string target, string value, int n)
        {
            int result = -1;

            Match m = Regex.Match(target, "((" + value + ").*?){" + n + "}");

            if (m.Success)
            {
                result = m.Groups[2].Captures[n - 1].Index;
            }

            return result;
        }

        public static string Slice(this string source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }

            int len = end - start;
            return source.Substring(start, len);
        }
    }
}
