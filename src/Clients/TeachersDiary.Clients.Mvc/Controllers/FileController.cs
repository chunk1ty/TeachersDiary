using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class FileController : BaseController
    {
        private readonly IExelParser _exelParser;

        public FileController(IExelParser exelParser)
        {
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
                    var fileName = Path.GetFileName(file.FileName);

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