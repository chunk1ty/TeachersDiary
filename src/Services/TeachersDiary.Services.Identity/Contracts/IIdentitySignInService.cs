using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Models;

namespace TeachersDiary.Services.Identity.Contracts
{
    public interface IIdentitySignInService : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(AspNetUser user, bool isPersistent, bool rememberBrowser);
    }
}
