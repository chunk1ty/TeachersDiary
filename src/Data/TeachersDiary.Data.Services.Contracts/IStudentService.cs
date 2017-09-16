using System.Threading.Tasks;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IStudentService
    {
        Task<StudentDomain> GetByIdAsync(string id);

        void Add(StudentDomain student);

        void Update(StudentDomain student);

        Task DeleteByIdAsync(string id);
    }
}
