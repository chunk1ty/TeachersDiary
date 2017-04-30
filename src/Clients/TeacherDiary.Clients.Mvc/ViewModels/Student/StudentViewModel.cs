using System;
using System.Collections.Generic;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping.Contracts;
using TeacherDiary.Data.Entities;


namespace TeacherDiary.Clients.Mvc.ViewModels.Student
{
    public class StudentViewModel : IMapFrom<Data.Entities.Student>, IMapTo<Data.Entities.Student>
    {
        public StudentViewModel()
        {
            Absences = new List<AbsenceViewModel>();
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public List<AbsenceViewModel> Absences { get; set; }
    }

    public class AbsenceViewModel : IMapFrom<Absence>, IMapTo<Absence>
    {
        public int Id { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}