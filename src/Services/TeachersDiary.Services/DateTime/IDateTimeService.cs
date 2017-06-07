namespace TeachersDiary.Services.DateTime
{
    public partial class DateTimeService
    {
        public interface IDateTimeService
        {
            System.DateTime UtcNow { get; }
        }
    }
}