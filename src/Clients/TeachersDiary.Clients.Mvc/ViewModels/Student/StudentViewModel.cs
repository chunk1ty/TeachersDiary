using System.Collections.Generic;

using TeachersDiary.Clients.Mvc.ViewModels.Absence;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Student
{
    public class StudentViewModel : IMapFrom<StudentDomain>, IMapTo<StudentDomain>
    {
        public StudentViewModel()
        {
            Absences = new List<AbsenceViewModel>();
        }

        public string EncodedId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string TotalExcusedAbsences { get; set; }

        public double TotalNotExcusedAbsences { get; set; }

        public string TotalNotExcusedAbsencesAsFractionNumber { get; set; }

        public List<AbsenceViewModel> Absences { get; set; }
    }
}