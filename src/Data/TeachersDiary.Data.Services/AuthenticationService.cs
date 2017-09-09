using System.Threading.Tasks;

using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Ef.Models;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;
        private readonly IEncryptingService _encryptingService;
        private readonly ITeacherService _teacherService;

        public AuthenticationService(
            IIdentitySignInService identitySignInService,
            IIdentityUserManagerService identityUserManagerService, 
            IEncryptingService encryptingService, ITeacherService teacherService)
        {
            Guard.WhenArgument(identitySignInService, nameof(identitySignInService)).IsNull().Throw();
            Guard.WhenArgument(identityUserManagerService, nameof(identityUserManagerService)).IsNull().Throw();

            _identitySignInService = identitySignInService;
            _identityUserManagerService = identityUserManagerService;
            _encryptingService = encryptingService;
            _teacherService = teacherService;
        }

        public async Task<IdentityResult> CreateAccountAsync(string email, string password, string selectedSchool)
        {
            var userEntity = new UserEntity()
            {
                Email = email,
                UserName = email,
            };

            var result = await _identityUserManagerService.CreateAsync(userEntity, password);

            if (result.Succeeded)
            {
                await _identitySignInService.SignInAsync(userEntity, false, false);

                if (selectedSchool != "-1")
                {
                    var teacherEntity = new TeacherDomain()
                    {
                        UserId = userEntity.Id,
                        SchoolId = _encryptingService.DecodeId(selectedSchool)
                    };

                    _teacherService.Add(teacherEntity);
                }
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
    }
}
