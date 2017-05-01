using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Contracts
{
    public interface IClassRepository
    {
        Task<Class> GetClassWithStudentsAndAbsencesByClassIdAsync(Guid classId);

        Task<IEnumerable<Class>> GetAllAsync();

        Task<Class> GetByIdAsync(Guid classId);

        void Add(Class system);

        void AddRange(List<Class> clases);

        void Delete(Class @class);
    }
}
