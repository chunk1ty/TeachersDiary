using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Contracts
{
    public interface IClassRepository
    {
        Task<ClassEntity> GetClassWithStudentsAndAbsencesByClassIdAsync(int classId);

        Task<IEnumerable<ClassEntity>> GetAllForUserAsync(string userId);

        Task<ClassEntity> GetByIdAsync(int classId);

        void Add(ClassEntity system);

        void AddRange(List<ClassEntity> clases);

        void Delete(ClassEntity @class);
    }
}
