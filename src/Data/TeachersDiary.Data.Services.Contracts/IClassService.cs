using System.Collections.Generic;
using System.Threading.Tasks;

using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDomain>> GetClassesBySchoolIdAsync(int schoolId);

        Task<ClassDomain> GetClassByClassIdAsync(string classId);

        void AddRange(List<ClassDomain> classes);

        void Add(ClassDomain @class);

        Task DeleteByIdAsync(string classId);
    }
}
