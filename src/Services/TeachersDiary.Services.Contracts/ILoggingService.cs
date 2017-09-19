using System;

namespace TeachersDiary.Services.Contracts
{
    public interface ILoggingService
    {
        void Error(string message, Exception ex);

        void Error(Exception ex);

        void Debug(string message);

        void Warning(string message);
    }
}
