using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        void Add(Class system);

        void AddRange(List<Class> classes);

        Task<IEnumerable<Class>> GetAllAsync();

        Task<Class> GetClassWithStudentsByClassIdAsync(Guid classId);

        Task DeleteById(Guid classId);
    }
}
