using System;
using System.Collections.Generic;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Services;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAbsenceService
    {
        void CalculateStudentsAbsencesForLastMonth(List<StudentDomain> students);
    }

    public class StudentDto
    {
        public StudentDto()
        {
            Absences = new HashSet<AbsenceEntity>();
        }

        public Guid Id { get; set; }

        public double TotalExcusedAbsences { get; set; }

        public double TotalNotExcusedAbsence { get; set; }

        public ICollection<AbsenceEntity> Absences { get; set; }
    }
}