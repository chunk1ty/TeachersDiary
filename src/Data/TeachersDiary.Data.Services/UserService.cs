using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class UserService : IUserService
    {
        private IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;
        private readonly ISchoolService _schoolService;

        public UserService(
            IIdentitySignInService identitySignInService,
            IIdentityUserManagerService identityUserManagerService, ISchoolService schoolService)
        {
            Guard.WhenArgument(identitySignInService, nameof(identitySignInService)).IsNull().Throw();
            Guard.WhenArgument(identityUserManagerService, nameof(identityUserManagerService)).IsNull().Throw();

            _identitySignInService = identitySignInService;
            _identityUserManagerService = identityUserManagerService;
            _schoolService = schoolService;
        }

        public void CreateAccountAsync()
        {
            //var result = await _identityUserManagerService.CreateAsync(user, model.Password);

            //if (result.Succeeded)
            //{
            //    await _identitySignInService.SignInAsync(user, false, false);
            //    await _identityUserManagerService.AddToRoleAsync(user.Id, ApplicationRole.Teacher);
            //}
        }
    }

    internal interface IUserService
    {
    }
}
