using System.IO;
using System.Web.Mvc;

using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Services.Contracts;
using TeacherDiary.Services;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassService _classService;

        public HomeController(IClassService classService)
        {
            _classService = classService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}