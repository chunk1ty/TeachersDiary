using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.School
{
    public class SchoolViewModel : IMap<SchoolDomain>
    {
        public string Name { get; set; }
    }
}