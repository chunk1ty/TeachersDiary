using System.Web.Mvc;
using AutoMapper;
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
        public ActionResult Index()
        {
            return View();
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

            return View();
        }
    }
}