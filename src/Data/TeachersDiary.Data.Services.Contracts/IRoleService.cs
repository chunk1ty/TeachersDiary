using System.Threading.Tasks;
using TeachersDiary.Common;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IRoleService
    {
        Task<OperationStatus> ChangeUserRoleAsync(string userId, ApplicationRoles role);
    }
}
