using System.Threading.Tasks;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Entities;
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

        public async Task ChangeUserRoleAsync(string userId, ApplicationRoles role)
        {
            var roles = new []
            {
                ApplicationRoles.Student.ToString(),
                ApplicationRoles.Teacher.ToString(),
                ApplicationRoles.Administrator.ToString(),
                ApplicationRoles.SchoolAdministrator.ToString(),
            };

            await _identityUserManagerService.RemoveFromRolesAsync(userId, roles);
            

            if (role != ApplicationRoles.None)
            {
                await _identityUserManagerService.AddToRoleAsync(userId, role.ToString());
            }
        }
    }
}
