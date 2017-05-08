using System.Collections.Generic;
using System.Threading.Tasks;
using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface ISchoolService
    {
        Task<IEnumerable<SchoolDomain>> GetAllSchoolNamesAsync();
    }
}
