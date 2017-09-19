using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Common;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDomain>> GetClassesBySchoolIdAsync(int schoolId);

        Task<ClassDomain> GetClassByClassIdAsync(string classId);

        OperationStatus Add(ClassDomain @class);

        Task DeleteByIdAsync(string classId);
    }
}
