using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using TeachersDiary.Data.Ef.Models;

namespace TeachersDiary.Data.Identity.Contracts
{
    public interface IIdentityUserManagerService : IDisposable
    {
        Task<IdentityResult> CreateAsync(UserEntity user, string password);

        Task<UserEntity> FindByIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> AddToRoleAsync(string userId, string role);

        Task<IEnumerable<UserEntity>> GetAllAsync();
    }
}
