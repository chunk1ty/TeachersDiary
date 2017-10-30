using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.User;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class SchoolAdminController : SchoolAdminAuthorizeController
    {
        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;
        private readonly IRoleService _roleService;

        public SchoolAdminController(
            IUserService userService, 
            IMappingService mappingService, 
            IRoleService roleService)
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
        public async Task<HttpStatusCodeResult> UserRole(string userId, string roleId)
        {
            var role = GetRole(roleId);

            var status = await _roleService.ChangeUserRoleAsync(userId, role);

            return status.IsSuccessful ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private ApplicationRoles GetRole(string roleId)
        {
            switch (roleId)
            {
                case "-1":
                    return ApplicationRoles.None;
                //case "1":
                //    return ApplicationRoles.Student;
                case "2":
                    return ApplicationRoles.Teacher;
                //case "3":
                //    return ApplicationRoles.SchoolAdministrator;
                //case "4":
                //    return ApplicationRoles.Administrator;
                default:
                    return ApplicationRoles.None;
            }
        }
    }
}