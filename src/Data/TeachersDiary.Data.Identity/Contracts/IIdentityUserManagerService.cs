using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Identity.Contracts
{
    public interface IIdentityUserManagerService : IDisposable
    {
        Task<IdentityResult> CreateAsync(UserEntity user, string password);

        Task<UserEntity> FindByIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> AddToRoleAsync(string userId, string role);
    }
}
