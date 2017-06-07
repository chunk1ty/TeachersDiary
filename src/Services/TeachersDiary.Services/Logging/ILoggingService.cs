namespace TeachersDiary.Services.Logging
{
    public interface ILoggingService
    {
        void Error(string message);

        void Debug(string message);

        void Warning(string message);
    }
}
