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
            double result;
            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            var split = fraction.Split(' ', '/');

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;
                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    int c;
                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }
    }
}
