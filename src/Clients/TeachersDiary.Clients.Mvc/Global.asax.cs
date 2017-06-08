using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using TeachersDiary.Services.Mapping;

namespace TeachersDiary.Clients.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngineConfig.RegisterViewEngine();
            AutoMapperConfig.RegisterAutomapper();
            DbConfig.RegisterDb();
            LoggingConfig.RegisterLog4Net();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
