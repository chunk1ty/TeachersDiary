using System.Collections.Generic;
using System.Threading.Tasks;

using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Contracts
{
    public interface IClassRepository
    {
        Task<ClassEntity> GetClassWithStudentsAndAbsencesByClassIdAsync(int classId);

        Task<IEnumerable<ClassEntity>> GetAllClassesForUserAsync(string userId);

        Task<ClassEntity> GetClassByIdAsync(int classId);

        void BulkInsert(List<ClassEntity> clases);

        void Delete(ClassEntity @class);
    }
}
