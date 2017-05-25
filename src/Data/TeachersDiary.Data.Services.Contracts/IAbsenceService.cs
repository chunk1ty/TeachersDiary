using System;
using System.Collections.Generic;
using TeachersDiary.Data.Services;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAbsenceService
    {
        void CalculateStudentsAbsencesForLastMonth(List<StudentDomain> students);
    }
}