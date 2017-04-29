using TeacherDiary.Clients.Mvc.Infrastructure.Mapping.Contracts;
using TeacherDiary.Data.Entities;


namespace TeacherDiary.Clients.Mvc.ViewModels.Student
{
    public class StudentViewModel : IMapFrom<Data.Entities.Student>, IMapTo<Data.Entities.Student>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
    }
}