using System.Web.Mvc;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}