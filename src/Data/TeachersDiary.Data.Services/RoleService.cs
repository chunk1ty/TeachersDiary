using System.Linq;
using System.Threading.Tasks;

using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class RoleService : IRoleService
    {
        private readonly IIdentityUserManagerService _identityUserManagerService;

        public RoleService(IIdentityUserManagerService identityUserManagerService)
        {
            _identityUserManagerService = identityUserManagerService;
        }

        public async Task<bool> IsChangeUserRoleSuccessfulAsync(string userId, ApplicationRoles role)
        {
            var roles = await _identityUserManagerService.GetRolesAsync(userId);

            if (roles.Count > 0)
            {
                var identityResult = await _identityUserManagerService.RemoveFromRolesAsync(userId, roles.ToArray());

                if (!identityResult.Succeeded)
                {
                    return false;
                }
            }

            if (role != ApplicationRoles.None)
            {
                var retuResult = await _identityUserManagerService.AddToRoleAsync(userId, role.ToString());

                return retuResult.Succeeded;
            }

            return true;
        }
    }
}
