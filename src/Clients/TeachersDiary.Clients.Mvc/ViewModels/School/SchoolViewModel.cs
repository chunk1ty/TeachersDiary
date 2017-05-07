using TeachersDiary.Data.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.School
{
    public class SchoolViewModel : IMapFrom<SchoolDomain>
    {
        public string Name { get; set; }
    }
}