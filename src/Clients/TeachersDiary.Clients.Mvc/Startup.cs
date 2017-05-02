using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeachersDiary.Clients.Mvc.Startup))]
namespace TeachersDiary.Clients.Mvc
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
