using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> CreateAccountAsync(string email, string password, string selectedSchool);
    }
}