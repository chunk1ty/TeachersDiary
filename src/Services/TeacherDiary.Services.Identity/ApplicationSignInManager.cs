//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using TeacherDiary.Services.Identity.Contracts;

//namespace TeacherDiary.Services.Identity
//{
//    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>, IIdentitySignInService
//    {
//        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
//           : base(userManager, authenticationManager)
//        {
//        }

//        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
//        {
//            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
//        }

//        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser aspNetUser)
//        {
//            return aspNetUser.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
//        }
//    }
//}