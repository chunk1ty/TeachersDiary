using System.Threading.Tasks;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IRoleService
    {
        Task<bool> IsChangeUserRoleSuccessfulAsync(string userId, ApplicationRoles role);
    }
}
