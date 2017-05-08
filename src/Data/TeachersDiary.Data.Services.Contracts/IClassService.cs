using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        void Add(ClassDomain system);

        void AddRange(List<ClassDomain> classes);

        Task<IEnumerable<ClassDomain>> GetAllAsync();

        Task<ClassDomain> GetClassWithStudentsByClassIdAsync(string classId);

        Task DeleteById(string classId);
    }
}
