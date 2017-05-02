using System.Reflection;
using TeachersDiary.Clients.Mvc.Infrastructure.Mapping;

namespace TeachersDiary.Clients.Mvc
{
    public class AutomapperConfig
    {
        public static void RegisterMap()
        {
            var authoMapper = new AutoMapperConfig();
            authoMapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}