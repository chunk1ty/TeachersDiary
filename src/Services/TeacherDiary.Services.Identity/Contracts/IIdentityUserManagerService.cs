using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TeacherDiary.Data.Ef;

namespace TeacherDiary.Services.Identity.Contracts
{
    public interface IIdentityUserManagerService : IDisposable
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);

        Task<bool> IsEmailConfirmedAsync(string userId);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);

        Task<ApplicationUser> FindByIdAsync(string userId);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(string userId);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);

        Task SendEmailAsync(string userId, string subject, string body);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    }
}
