using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.User;
using TeachersDiary.Common.Constants;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    [Authorize(Roles = ApplicationRole.SchoolAdministrator)]
    public class SchoolAdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;
        private readonly IRoleService _roleService;
        private readonly ILoggingService _loggingService;

        public SchoolAdminController(IUserService userService, IMappingService mappingService, IRoleService roleService, ILoggingService loggingService)
        {
            _userService = userService;
            _mappingService = mappingService;
            _roleService = roleService;
            _loggingService = loggingService;
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
            try
            {
                var role = GetRole(roleId);

                var result = await _roleService.IsChangeUserRoleSuccessfulAsync(userId, role);

                return result ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex.Message, ex);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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