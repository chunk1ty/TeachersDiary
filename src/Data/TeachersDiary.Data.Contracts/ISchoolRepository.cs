using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Contracts
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<SchoolEntity>> GetAllSchoolNamesAsync();
    }
}
