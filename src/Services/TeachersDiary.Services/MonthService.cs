using System;
using System.Collections.Generic;
using TeachersDiary.Common;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Services
{
    public class MonthService : IMonthService
    {
        public List<Month> GetCurrentAndNextMonth()
        {
            var monthId = DateTime.UtcNow.Month;
            var months = new List<Month>();

            months.Add(GetMonth(monthId));
            months.Add(monthId != 12 ? GetMonth(monthId + 1) : GetMonth(1));

            return months;
        }

        private Month GetMonth(int monthId)
        {
            switch (monthId)
            {
                case 9:
                    return new Month(9, "Септември");
                case 10:
                    return new Month(10, "Октомври");
                case 11:
                    return new Month(11, "Ноември");
                case 12:
                    return new Month(12, "Декември");
                case 1:
                    return new Month(13, "Януари");
                case 2:
                    return new Month(14, "Февруари");
                case 3:
                    return new Month(15, "Март");
                case 4:
                    return new Month(16, "Април");
                case 5:
                    return new Month(17, "Май");
                case 6:
                    return new Month(18, "Юни");
                default:
                    throw new InvalidOperationException(nameof(monthId));
            }
        }
    }
}
