using System.Web.Mvc;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class LandingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}