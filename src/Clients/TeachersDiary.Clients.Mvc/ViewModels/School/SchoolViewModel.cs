using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.School
{
    public class SchoolViewModel : IMap<SchoolDomain>
    {
        public string Name { get; set; }
    }
}