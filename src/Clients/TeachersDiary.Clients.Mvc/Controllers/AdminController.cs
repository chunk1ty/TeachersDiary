using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Clients.Mvc.ViewModels.User;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    [Authorize(Roles = ApplicationRole.Administrator)]
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;

        public AdminController(IUserService userService, IMappingService mappingService)
        {
            _userService = userService;
            _mappingService = mappingService;
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllAsync();

            var usersViewModel = _mappingService.Map<IList<UserViewModel>>(users);

            return View(usersViewModel);
        }
    }
}