using System.Reflection;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping;

namespace TeacherDiary.Clients.Mvc
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