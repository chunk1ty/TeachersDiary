using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using TeachersDiary.Clients.Mvc.ViewModels.Account;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        private IIdentityUserManagerService _identityUserManagerService;
        private readonly IIdentitySignInService _identitySignInService;
        private readonly ISchoolService _schoolService;

        public AccountController(
            IIdentitySignInService identitySignInService,
            IIdentityUserManagerService identityUserManagerService, ISchoolService schoolService)
        {
            Guard.WhenArgument(identitySignInService, nameof(identitySignInService)).IsNull().Throw();
            Guard.WhenArgument(identityUserManagerService, nameof(identityUserManagerService)).IsNull().Throw();

            _identitySignInService = identitySignInService;
            _identityUserManagerService = identityUserManagerService;
            _schoolService = schoolService;
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
        public async Task<ActionResult> Register()
        {
            var schoolNames = await _schoolService.GetAllSchoolNamesAsync();
            var schoolLists = schoolNames.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            schoolLists.Add(new SelectListItem()
            {
                Text = "Друго",
                Value = "-1"
            });

            RegisterViewModel model = new RegisterViewModel()
            {
                Schools = schoolLists
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.SelectedSchool == null)
            {
                // TODO bind to school property
                ModelState.AddModelError("model.SelectedSchool", "Моля изберете училище!");
                return View(model);
            }

            var user = new UserEntity();

            user.UserName = model.Email;
            user.Email = model.Email;
            user.SchoolId = model.SelectedSchool != null && model.SelectedSchool != "-1" ? int.Parse(model.SelectedSchool) : 0;

            var result = await _identityUserManagerService.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _identitySignInService.SignInAsync(user, false, false);
                await _identityUserManagerService.AddToRoleAsync(user.Id, ApplicationRole.Teacher);

                return this.RedirectToAction<AccountController>(c => c.Login(string.Empty));
            }

            AddErrors(result);

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