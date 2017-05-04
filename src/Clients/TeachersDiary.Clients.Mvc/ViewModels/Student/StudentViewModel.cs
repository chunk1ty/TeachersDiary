using System;
using System.Collections.Generic;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Services;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Mapping.Contracts;


namespace TeachersDiary.Clients.Mvc.ViewModels.Student
{
    public class StudentViewModel : IMapFrom<StudentEntity>, IMapTo<StudentEntity>, IMapTo<StudentDto>
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

        public double TotalExcusedAbsences { get; set; }
        
        public double TotalNotExcusedAbsence { get; set; }

        public string TotalNotExcusedAbsenceAsString { get; set; }
    }

    public class AbsenceViewModel : IMapFrom<AbsenceEntity>, IMapTo<AbsenceEntity>
    {
        public int Id { get; set; }

        public Guid StudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}