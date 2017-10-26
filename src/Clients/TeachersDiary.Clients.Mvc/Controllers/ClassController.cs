using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.Infrastructure.Attribute;
using TeachersDiary.Clients.Mvc.Infrastructure.Session;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class ClassController : TeacherController
    {
        private readonly IClassService _classService;
        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;
        private readonly ISessionStateService _sessionStateService;

        public ClassController(
            IClassService classService, 
            IMappingService mappingService, 
            IUserService userService, ISessionStateService sessionStateService)
        {
            _classService = classService;
            _mappingService = mappingService;
            _userService = userService;
            _sessionStateService = sessionStateService;
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            var session = await _sessionStateService.GetAsync(HttpContext);

            var classDomains =  await _classService.GetClassesBySchoolIdAsync(1);

            var classViewModels = _mappingService.Map<IList<ClassViewModel>>(classDomains);

            return View(classViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string classId)
        {
            await _classService.DeleteByIdAsync(classId);

            return this.RedirectToAction<ClassController>(x => x.All());
        }

        [HttpGet]
        [TeachersDiaryAuthorize(ApplicationRoles.SchoolAdministrator)]
        public async Task<ActionResult> Create()
        {
            var createClassViewModel = new CreateClassViewModel();

            await GetTeachersBySchoolIdAsync(createClassViewModel);

            return View(createClassViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TeachersDiaryAuthorize(ApplicationRoles.SchoolAdministrator)]
        public async Task<ActionResult> Create(CreateClassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await GetTeachersBySchoolIdAsync(model);

                return View(model);
            }

            var classDomain = _mappingService.Map<ClassDomain>(model);
            var status = _classService.Add(classDomain);

            if (!status.IsSuccessful)
            {
                ModelState.AddModelError("", status.Message);

                await GetTeachersBySchoolIdAsync(model);
                
                return View(model);
            }

            return this.RedirectToAction<ClassController>(x => x.All());
        }

        private async Task GetTeachersBySchoolIdAsync(CreateClassViewModel createClassViewModel)
        {
            var teachers = await _userService.GetTeachersBySchoolIdAsync();
            createClassViewModel.Teachers = new SelectList(teachers, "Id", "FullName", 1);
        }
    }
}