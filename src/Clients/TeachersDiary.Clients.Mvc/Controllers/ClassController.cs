using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using Microsoft.AspNet.Identity;

using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;
using TeachersDiary.Services.ExcelParser;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class ClassController : TeacherController
    {
        //TODO IExelParser injection ??
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
        public async Task<ActionResult> All()
        {
            var userId = User.Identity.GetUserId();

            var classDomains =  await _classService.GetAllAvailableClassesForUserAsync(userId);

            var classViewModels = _mappingService.Map<IList<ClassViewModel>>(classDomains);

            return View(classViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> Index(string classId)
        {
            var classDomain = await _classService.GetClassWithStudentsByClassIdAsync(classId);

            User.Identity.GetUserId();
            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);

            return View(classViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string classId)
        {
            await _classService.DeleteByIdAsync(classId);

            return this.RedirectToAction<ClassController>(x => x.All());
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
            var extenstion = Path.GetExtension(file.FileName);

            if (file.ContentLength > 0)
            {
                // xls - 98 - 03
                if (extenstion == ".xlsx")
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);

                    var userId = User.Identity.GetUserId();

                    _exelParser.CreateClassForUser(path, userId);

                    return this.RedirectToAction<ClassController>(x => x.All());
                }

                ModelState.AddModelError("", Resources.Resources.IncorrectFileFormat);
            }

            return View();
        }
    }
}