using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToFractionalNumber(this double number)
        {
            var integerPart = (int)number;
            var floatingPart = number - Math.Truncate(number);

            var floatingPartAstring = floatingPart.ToString();
            var fractionalPart = string.Empty;

            if (floatingPartAstring.Contains("3333"))
            {
                fractionalPart = " 1/3";
            }

            if (floatingPartAstring.Contains("6666"))
            {
                fractionalPart = " 2/3";
            }

            if (floatingPartAstring.Contains("9999"))
            {
                integerPart++;
            }

            var result = integerPart != 0 ? integerPart + fractionalPart : fractionalPart == string.Empty ? "0" : fractionalPart;

            return result;
        }
    }
}