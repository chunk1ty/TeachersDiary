using System.Threading.Tasks;
using TeachersDiary.Common;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IStudentService
    {
        Task<StudentDomain> GetByIdAsync(string id);

        OperationStatus Add(StudentDomain student);

        OperationStatus Update(StudentDomain student);

        Task DeleteByIdAsync(string id);
    }
}
