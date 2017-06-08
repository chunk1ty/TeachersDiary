using System;

using Common.Logging;

namespace TeachersDiary.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILog _logger = LogManager.GetLogger<LoggingService>();

        public void Error(string message, Exception ex)
        {
            _logger.Error(x => x(message), ex);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }
    }
}
