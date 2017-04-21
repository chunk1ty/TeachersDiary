using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeacherDiary.Clients.Mvc.Startup))]
namespace TeacherDiary.Clients.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
