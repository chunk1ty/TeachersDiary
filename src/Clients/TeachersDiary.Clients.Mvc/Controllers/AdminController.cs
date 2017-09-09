using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.User;
using TeachersDiary.Common.Constants;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    [Authorize(Roles = ApplicationRole.Administrator)]
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;
        private readonly IRoleService _roleService;

        public AdminController(IUserService userService, IMappingService mappingService, IRoleService roleService)
        {
            _userService = userService;
            _mappingService = mappingService;
            _roleService = roleService;
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllAsync();

            var usersViewModel = _mappingService.Map<IList<UserViewModel>>(users);

            return View(usersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UserRole(string userId, string roleId)
        {
            var role = GetRole(roleId);
           
            await _roleService.ChangeUserRoleAsync(userId, role);

            return Json(new object());
        }

        private ApplicationRoles GetRole(string roleId)
        {
            switch (roleId)
            {case "-1":
                    return ApplicationRoles.None;

                case "1":
                    return ApplicationRoles.Student;

                case "2":
                    return ApplicationRoles.Teacher;

                case "3":
                    return ApplicationRoles.SchoolAdministrator;

                case "4":
                    return ApplicationRoles.Administrator;
                default:
                    return ApplicationRoles.None;
            }
        }
    }
}