using System.Web.Mvc;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}