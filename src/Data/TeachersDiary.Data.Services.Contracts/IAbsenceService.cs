using System.Collections.Generic;

using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAbsenceService
    {
        void CalculateStudentAbsences(List<StudentDomain> students, string monthId);
    }
}