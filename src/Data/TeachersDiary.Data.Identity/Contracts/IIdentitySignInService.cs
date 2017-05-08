using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Identity.Contracts
{
    public interface IIdentitySignInService : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(UserEntity user, bool isPersistent, bool rememberBrowser);
    }
}
