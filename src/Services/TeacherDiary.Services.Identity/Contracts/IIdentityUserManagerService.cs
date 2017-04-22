using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TeacherDiary.Data.Ef;

namespace TeacherDiary.Services.Identity.Contracts
{
    public interface IIdentityUserManagerService : IDisposable
    {
        Task<IdentityResult> CreateAsync(AspNetUser user, string password);

        Task<AspNetUser> FindByIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
