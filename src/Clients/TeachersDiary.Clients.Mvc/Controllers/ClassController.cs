using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Microsoft.AspNet.Identity;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.ExcelParser;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    [Authorize(Roles = ApplicationRole.Teacher)]
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IMappingService _mappingService;
        private readonly IExelParser _exelParser;

        public ClassController(
            IClassService classService, 
            IMappingService mappingService, 
            IExelParser exelParser)
        {
            _classService = classService;
            _mappingService = mappingService;
            _exelParser = exelParser;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            var classDomains =  await _classService.GetAllAvailableClassesForUserAsync(userId);

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
        public async Task<ActionResult> Delete(string classId)
        {
            await _classService.DeleteById(classId);

            return this.RedirectToAction<ClassController>(x => x.Index());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file)
        {
            string extenstion = Path.GetExtension(file.FileName);

            if (file != null && file.ContentLength > 0)
            {
                // xls - 98 - 03
                if (extenstion == ".xlsx")
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);

                    var userId = User.Identity.GetUserId();

                    _exelParser.CreateClassForUser(path, userId);

                    return this.RedirectToAction<ClassController>(x => x.Index());
                }

                ModelState.AddModelError("", "Невалиден файл формат!");
            }

            return View();
        }
    }
}