using System;
using System.Linq;

namespace TeachersDiary.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsContainsOnlyDigits(this string value)
        {
            return value.All(c => c >= '0' && c <= '9');
        }

        public static double FractionToDoubleNumber(this string fraction)
        {
            if (double.TryParse(fraction, out double result))
            {
                return result;
            }

            var split = fraction.Split(' ', '/');

            if (split.Length == 2 || split.Length == 3)
            {
                if (int.TryParse(split[0], out int a) && int.TryParse(split[1], out int b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    if (int.TryParse(split[2], out int c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }
    }
}
