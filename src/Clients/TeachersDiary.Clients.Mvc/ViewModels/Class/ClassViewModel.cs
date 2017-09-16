using System.Collections.Generic;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Clients.Mvc.ViewModels.User;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.Class
{
    public class ClassViewModel : IMap<ClassDomain>
    {
        public ClassViewModel()
        {
            Students = new List<StudentViewModel>();
        }

        public string Id { get; set; }
       
        public string Name { get; set; }

        public double TotalExcusedAbsences { get; set; }

        public double TotalNotExcusedAbsences { get; set; }

        public List<StudentViewModel> Students { get; set; }

        public UserViewModel ClassTeacher { get; set; }
    }
}