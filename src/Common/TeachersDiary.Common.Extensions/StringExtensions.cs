using System.Linq;

namespace TeachersDiary.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsContainsOnlyDigits(this string value)
        {
            return value.All(c => c >= '0' && c <= '9');
        }
    }
}
