using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Services
{
    public class DateTimeService : IDateTimeService
    {
        public System.DateTime UtcNow
        {
            get { return System.DateTime.UtcNow; }
        }
    }
}
