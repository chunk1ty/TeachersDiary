using System;

namespace TeachersDiary.Services.Logging
{
    public interface ILoggingService
    {
        void Error(string message, Exception ex);

        void Debug(string message);

        void Warning(string message);
    }
}
