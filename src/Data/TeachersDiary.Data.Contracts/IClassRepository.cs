using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Contracts
{
    public interface IClassRepository
    {
        Task<ClassEntity> GetClassWithStudentsAndAbsencesByClassIdAsync(Guid classId);

        Task<IEnumerable<ClassEntity>> GetAllAsync();

        Task<ClassEntity> GetByIdAsync(Guid classId);

        void Add(ClassEntity system);

        void AddRange(List<ClassEntity> clases);

        void Delete(ClassEntity @class);
    }
}
