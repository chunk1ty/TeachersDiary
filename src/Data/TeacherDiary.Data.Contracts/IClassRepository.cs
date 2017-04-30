using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Contracts
{
    public interface IClassRepository
    {
        void Add(Class system);

        void AddRange(List<Class> clases);

        Task<Class> GetClassWithStudentsAndAbsencesByClassIdAsync(Guid classId);

        Task<IEnumerable<Class>> GetAllAsync();
    }
}
