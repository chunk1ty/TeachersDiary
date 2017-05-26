using System.Collections.Generic;

using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAbsenceService
    {
        void CalculateStudentsAbsencesForLastMonth(List<StudentDomain> students);
    }
}