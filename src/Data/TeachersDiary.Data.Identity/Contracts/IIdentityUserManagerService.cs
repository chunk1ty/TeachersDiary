using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Identity.Contracts
{
    public interface IIdentityUserManagerService
    {
        Task<IdentityResult> CreateAsync(UserEntity user, string password);

        Task<UserEntity> FindByIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> AddToRoleAsync(string userId, string role);

        Task<IdentityResult> RemoveFromRoleAsync(string userId, string role);
     
        Task<IdentityResult> RemoveFromRolesAsync(string userId, params string[] roles);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<IList<string>> GetRolesAsync(string userId);
    }
}
