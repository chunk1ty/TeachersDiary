using System.Threading.Tasks;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Encrypting;

namespace TeachersDiary.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
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
                userEntity.SchoolId = _encryptingService.DecodeId(selectedSchool);
            }

            var result = await _identityUserManagerService.CreateAsync(userEntity, password);

            if (result.Succeeded)
            {
                await _identitySignInService.SignInAsync(userEntity, false, false);
                await _identityUserManagerService.AddToRoleAsync(userEntity.Id, ApplicationRole.Teacher);
            }

            return result;
        }
    }
}
