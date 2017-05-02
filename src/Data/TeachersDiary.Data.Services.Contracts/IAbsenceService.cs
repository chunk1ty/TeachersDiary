using System;
using System.Collections.Generic;

using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAbsenceService
    {
        void CalculateStudentsAbsencesForLastMonth(List<StudentDto> students);
    }

    public class StudentDto
    {
        public StudentDto()
        {
            Absences = new HashSet<Absence>();
        }

        public Guid Id { get; set; }

        public double TotalExcusedAbsences { get; set; }

        public double TotalNotExcusedAbsence { get; set; }

        public ICollection<Absence> Absences { get; set; }
    }
}