using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Contracts
{
    public interface IClassRepository
    {
        void Add(Class system);

        Task<IEnumerable<Class>> GetAllWithStudentsAsync();

        Task<Class> GetClassWithStudentsByClassIdAsync(Guid classId);
    }
}
