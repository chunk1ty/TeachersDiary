using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using AutoMapper;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping;
using TeacherDiary.Clients.Mvc.ViewModels.Class;
using TeacherDiary.Data.Entities;
using TeacherDiary.Data.Services.Contracts;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var classesAsDbEntities =  await _classService.GetAllAsync();

            var classesAsViewModel = classesAsDbEntities.To<ClassViewModel>().ToList();

            return View(classesAsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Students(Guid classId)
        {
            var classAsDbEntities = await _classService.GetClassWithStudentsByClassIdAsync(classId);

            var classAsViewModel = Mapper.Map<ClassViewModel>(classAsDbEntities);

            return View(classAsViewModel);
        }

        [HttpPost]
        public ActionResult Students(ClassViewModel model)
        {

            return this.RedirectToAction<ClassController>(x => x.Students(model.Id));
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

            var classAsDbEntity = Mapper.Map<Class>(model);

            _classService.Add(classAsDbEntity);

            return this.RedirectToAction<ClassController>(x => x.Index());
        }
    }
}