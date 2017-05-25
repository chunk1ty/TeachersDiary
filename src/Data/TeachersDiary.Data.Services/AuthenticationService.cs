using System.Threading.Tasks;
using System.Web;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Ef.Models;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        // TODO check IIdentityUserManagerService IIdentitySignInService do they have to be Disposable
        private readonly IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;
        private readonly IEncryptingService _encryptingService;

        public AuthenticationService(
            IIdentitySignInService identitySignInService,
            IIdentityUserManagerService identityUserManagerService, 
            IEncryptingService encryptingService)
        {
            Guard.WhenArgument(identitySignInService, nameof(identitySignInService)).IsNull().Throw();
            Guard.WhenArgument(identityUserManagerService, nameof(identityUserManagerService)).IsNull().Throw();

            _identitySignInService = identitySignInService;
            _identityUserManagerService = identityUserManagerService;
            _encryptingService = encryptingService;
        }

        public async Task<IdentityResult> CreateAccountAsync(string email, string password, string selectedSchool)
        {
            var userEntity = new UserEntity()
            {
                Email = email,
                UserName = email,
            };

            if (selectedSchool != "-1")
            {
                //userEntity.SchoolId = _encryptingService.DecodeId(selectedSchool);
            }

            var result = await _identityUserManagerService.CreateAsync(userEntity, password);

            if (result.Succeeded)
            {
                await _identityUserManagerService.AddToRoleAsync(userEntity.Id, ApplicationRole.Teacher);
                await _identitySignInService.SignInAsync(userEntity, false, false);
            }

            return result;
        }

        public async Task<SignInStatus> LogIn(string email, string password)
        {
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            return await _identitySignInService.PasswordSignInAsync(email, password, false, false);
        }

        public async Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var result = await _identityUserManagerService.ChangePasswordAsync(userId, oldPassword, newPassword);

            if (result.Succeeded)
            {
                var user = await _identityUserManagerService.FindByIdAsync(userId);

                if (user != null)
                {
                    await _identitySignInService.SignInAsync(user, false, false);
                }
            }

            return result;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_identityUserManagerService != null)
        //        {
        //            _identityUserManagerService.Dispose();
        //            _identityUserManagerService = null;
        //        }

        //        if (_identityUserManagerService != null)
        //        {
        //            _identityUserManagerService.Dispose();
        //            _identityUserManagerService = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
    }
}
