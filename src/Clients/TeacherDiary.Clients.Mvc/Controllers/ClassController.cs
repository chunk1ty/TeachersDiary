using System.Web.Mvc;
using TeacherDiary.Clients.Mvc.ViewModels.Class;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class ClassController : Controller
    {
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

            return View();
        }
    }
}