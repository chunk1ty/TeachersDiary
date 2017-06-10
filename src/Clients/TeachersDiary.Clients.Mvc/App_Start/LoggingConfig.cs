using log4net;

namespace TeachersDiary.Clients.Mvc
{
    public class LoggingConfig
    {
        public static void RegisterLog4Net()
        {
            GlobalContext.Properties["ApplicationId"] = "TeachersDiary.Clietns.Mvc";
        }
    }
}