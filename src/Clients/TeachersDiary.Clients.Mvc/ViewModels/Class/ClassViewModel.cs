using System.Collections.Generic;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Class
{
    public class ClassViewModel : IMap<ClassDomain>
    {
        public ClassViewModel()
        {
            Students = new List<StudentViewModel>();
        }

        public string EncodedId { get; set; }
       
        public string Name { get; set; }

        public double TotalExcusedAbsences { get; set; }

        public double TotalNotExcusedAbsences { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}