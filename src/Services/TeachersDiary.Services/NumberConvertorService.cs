using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Services
{
    public class NumberConvertorService : INumberConvertorService
    {
        public string FromDoubleToFractionNumber(double number)
        {
            var integerPart = (int)number;
            var floatingPart = number - Math.Truncate(number);

            var floatingPartAstring = floatingPart.ToString();
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

        public double FromFractionToDoubleNumber(string fraction)
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
