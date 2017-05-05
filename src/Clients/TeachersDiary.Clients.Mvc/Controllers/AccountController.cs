using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using TeachersDiary.Clients.Mvc.ViewModels.Account;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Identity.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        private IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;

        public AccountController(
            IIdentitySignInService identitySignInService,
            IIdentityUserManagerService identityUserManagerService)
        {
            Guard.WhenArgument(identitySignInService, nameof(identitySignInService)).IsNull().Throw();
            Guard.WhenArgument(identityUserManagerService, nameof(identityUserManagerService)).IsNull().Throw();

            _identitySignInService = identitySignInService;
            _identityUserManagerService = identityUserManagerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction<DashboardController>(x => x.Index());
            }

            // TODO do i need returnUrl ?
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _identitySignInService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result == SignInStatus.Success)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Невалиден имейл или парола.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            TeachersDiaryDbContext dbContext = new TeachersDiaryDbContext();
            var roles = dbContext.Roles.OrderBy(x => x.Name);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AspNetUser();

                user.UserName = model.Email;
                user.Email = model.Email;

                var result = await _identityUserManagerService.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _identitySignInService.SignInAsync(user, false, false);

                    return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
                }

                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _identityUserManagerService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await _identityUserManagerService.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await _identitySignInService.SignInAsync(user, false, false);
                }

                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            AddErrors(result);

            return this.View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_identityUserManagerService != null)
                {
                    _identityUserManagerService.Dispose();
                    _identityUserManagerService = null;
                }

                if (_identityUserManagerService != null)
                {
                    _identityUserManagerService.Dispose();
                    _identityUserManagerService = null;
                }
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return this.RedirectToAction<DashboardController>(x => x.Index());
        }
    }
}