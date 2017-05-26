using System;
using System.Globalization;

namespace TeachersDiary.Common.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToFractionNumber(this double number)
        {
            var integerPart = (int)number;
            var floatingPart = number - Math.Truncate(number);

            var floatingPartAstring = floatingPart.ToString(CultureInfo.InvariantCulture);
            var fractionalPart = string.Empty;

            if (floatingPartAstring.Contains("3333"))
            {
                fractionalPart = "1/3";
            }

            if (floatingPartAstring.Contains("6666"))
            {
                fractionalPart = "2/3";
            }

            if (floatingPartAstring.Contains("9999"))
            {
                integerPart++;
            }

            string result;

            if (integerPart != 0)
            {
                result = fractionalPart == string.Empty ? integerPart.ToString() : integerPart + " " + fractionalPart;
            }
            else
            {
                result = fractionalPart == string.Empty ? "0" : fractionalPart;
            }

            return result;
        }
    }
}
