using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Domain;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    [Authorize(Roles = ApplicationRole.Teacher)]
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IMappingService _mappingService;

        public ClassController(
            IClassService classService, 
            IMappingService mappingService)
        {
            _classService = classService;
            _mappingService = mappingService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var classDomains =  await _classService.GetAllAsync();

            var classViewModels = _mappingService.Map<IEnumerable<ClassViewModel>>(classDomains);

            return View(classViewModels);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ClassViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }

            var classDomain = _mappingService.Map<ClassDomain>(model);

            _classService.Add(classDomain);

            return this.RedirectToAction<ClassController>(x => x.Index());
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int classId)
        {
            await _classService.DeleteById(classId);

            return this.RedirectToAction<ClassController>(x => x.Index());
        }
    }
}