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

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IClassService _classService;

        public DashboardController(IClassService classService)
        {
            _classService = classService;
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
            if(file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                file.SaveAs(path);

                var reader = new ExelParser(_classService);

                reader.ReadFile(path);
            }
            
            return RedirectToAction("Index");
        }
    }
}