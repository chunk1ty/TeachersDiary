using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TeacherDiary.Clients.Mvc.ViewModels.Account;
using TeacherDiary.Data.Ef;
using TeacherDiary.Services.Identity.Contracts;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;

        public AccountController(
            IIdentitySignInService identitySignInService,
            IIdentityUserManagerService identityUserManagerService)
        {
            _identitySignInService = identitySignInService; //?? throw new ArgumentNullException(nameof(identitySignInService));
            _identityUserManagerService = identityUserManagerService; //?? throw new ArgumentNullException(nameof(identityUserManagerService));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                user.UserName = model.Email;
                user.Email = model.Email;

                var result = await _identityUserManagerService.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // generate token
                    //await SendEmalForNewUser(user);
                    //AddNotification("Проверете имейлът си за да активирате акаунта.", NotificationType.Warning);

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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _identityUserManagerService.FindByNameAsync(model.Email);

                if (user == null || !(await _identityUserManagerService.IsEmailConfirmedAsync(user.Id)))
                {
                    //this.AddNotification("Въведеният имейл не съществува.", NotificationType.Error);

                    return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
                }

                //await SendEmailForForgottenPasswordAsync(user);

                //this.AddNotification("Проверете вашият имейл за въвеждане на нова парола.", NotificationType.Info);

                return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _identityUserManagerService.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("IncorectEmail", "Имейл адресът не съществува.");

                return View(model);
            }

            var result = await _identityUserManagerService.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                //this.AddNotification("Успешно променихте вашата парола.", NotificationType.Success);

                return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
            }

            ModelState.AddModelError(string.Empty, "Невалиден имейл адрес или токен.");

            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await _identityUserManagerService.ConfirmEmailAsync(userId, code);

            //if (result.Succeeded)
            //{
            //    this.AddNotification("Успешно активирахте вашият акаунт.", NotificationType.Success);
            //}
            //else
            //{
            //    this.AddNotification("Неуспешно активирахте вашият акаунт.", NotificationType.Error);
            //}

            return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
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

            return this.RedirectToAction<HomeController>(x => x.Index());
        }
    }
}