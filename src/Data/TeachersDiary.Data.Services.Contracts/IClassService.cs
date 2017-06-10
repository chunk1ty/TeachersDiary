using System.Collections.Generic;
using System.Threading.Tasks;

using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDomain>> GetAllAvailableClassesForUserAsync(string userId);

        Task<ClassDomain> GetClassWithStudentsByClassIdAsync(string classId);

        void AddRange(List<ClassDomain> classes);

        Task DeleteByIdAsync(string classId);
    }
}
