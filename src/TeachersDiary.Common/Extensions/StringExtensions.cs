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

        public static double ToDoubleNumber(this string fraction)
        {
            var input = fraction.Split(' ');

            if (input.Length == 1)
            {
                int result;
                if (int.TryParse(fraction, out result))
                {
                    return result;
                }

                var fractionNumber = fraction.Split('/');
                int divisible, divider;

                if (int.TryParse(fractionNumber[0], out divisible) &&
                    int.TryParse(fractionNumber[1], out divider))
                {
                    if ((divisible == 1 || divisible == 2) && divider == 3)
                    {
                        var fractionalNumberAsDouble = (double)divisible / divider;

                        return fractionalNumberAsDouble;
                    }
                }
            }

            if (input.Length == 2)
            {
                var fractionNumber = input[1].Split('/');

                if (fractionNumber.Length == 2)
                {
                    // делимо       делител
                    int integerpart, divisible, divider;

                    if (int.TryParse(input[0], out integerpart) &&
                        int.TryParse(fractionNumber[0], out divisible) && 
                        int.TryParse(fractionNumber[1], out divider))
                    {
                        if ((divisible == 1 || divisible == 2) && divider == 3)
                        {
                            var fractionalNumberAsDouble = (double)divisible / divider;

                            var result = integerpart + fractionalNumberAsDouble;

                            return result;
                        }
                       
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }

        public static bool IsPositiveDoubleNumber(this string value)
        {
            double number;
            var isDouble = double.TryParse(value, out number);

            if (!isDouble)
            {
                return false;
            }

            var isPositiveNumber = number >= 0;

            return isPositiveNumber;
        }

        public static bool IsPositiveFractionNumber(this string value)
        {
            bool result = true;

            double number; 

            try
            {
                number = value.ToDoubleNumber();
            }
            catch (FormatException)
            {
                result = false;

            }

            var isPositiveNumber = number >= 0; 

            return isPositiveNumber;
        }
    }
}
