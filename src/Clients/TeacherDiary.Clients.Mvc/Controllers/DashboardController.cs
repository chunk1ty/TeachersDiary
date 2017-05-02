using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Services.Contracts;
using TeacherDiary.Services;
using TeacherDiary.Services.Contracts;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IExelParser _exelParser;

        public DashboardController(IClassService classService, IExelParser exelParser)
        {
            _classService = classService;
            _exelParser = exelParser;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string extenstion = Path.GetExtension(file.FileName);

            if (file != null && file.ContentLength > 0)
            {
                if (extenstion == ".xls" || extenstion == ".xlsx")
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);

                    _exelParser.ReadFile(path);

                    return this.RedirectToAction<ClassController>(x => x.Index());
                }
              
                ModelState.AddModelError("", "Невалиден файл формат!");
            }

            return View();
        }
    }
}