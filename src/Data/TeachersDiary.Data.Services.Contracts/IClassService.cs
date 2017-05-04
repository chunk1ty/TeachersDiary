using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        void Add(ClassEntity system);

        void AddRange(List<ClassEntity> classes);

        Task<IEnumerable<ClassEntity>> GetAllAsync();

        Task<ClassEntity> GetClassWithStudentsByClassIdAsync(Guid classId);

        Task DeleteById(Guid classId);
    }
}
