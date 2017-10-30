using System.Web.Mvc;

using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Filters
{
    public class TeachersDiaryExeptionAttribute : HandleErrorAttribute
    {
        private readonly ILoggingService _loggingService;

        public TeachersDiaryExeptionAttribute(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (filterContext.Exception != null)
            {
                _loggingService.Error(filterContext.Exception);
            }
        }
    }
}