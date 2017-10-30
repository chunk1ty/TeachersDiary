using System.Web.Mvc;

using TeachersDiary.Clients.Mvc.Infrastructure.Filters;
using TeachersDiary.Services;

namespace TeachersDiary.Clients.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TeachersDiaryExeptionAttribute(new LoggingService()));
        }
    }
}
