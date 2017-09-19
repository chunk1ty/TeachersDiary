using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> CreateAccountAsync(string email, string password, string firstName, string middleName, string lastName,string selectedSchool);

        Task<SignInStatus> LogIn(string email, string password);

        Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}