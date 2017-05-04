using System.Reflection;
using TeachersDiary.Services.Mapping;

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