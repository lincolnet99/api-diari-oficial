using System;

namespace Regulatorio.SharedKernel.Common
{
    public static class CustomString
    {
        public static string RandomString()
        {
            var rd = new Random();
            var code = "da39a3ee5e6b4b0d3255bfef95601890af";

            var upperCase = code.Substring(0, rd.Next(4, 8)).ToUpper();
            var lowerCase = code.Substring(8, rd.Next(4, 8)).ToLower();
            var digit = rd.Next(10, 99).ToString();

            code = string.Concat(upperCase, lowerCase, digit);
            code = code.Insert(rd.Next(0, code.Length), "@");

            return code;
        }
    }
}