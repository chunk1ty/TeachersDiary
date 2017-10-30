using System.Collections.Generic;
using System.Threading.Tasks;

using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDomain>> GetAllAsync();

        Task<IEnumerable<UserDomain>> GetTeachersBySchoolIdAsync();

        Task<UserDomain> GetUserByIdAsync(string id);
    }
}
