using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Common;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAbsenceService
    {
        void CalculateStudentAbsences(List<StudentDomain> students, string monthId);

        Task<OperationStatus> DeleteByClassAsyncId(string classId);
    }
}