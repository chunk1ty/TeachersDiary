using System.Web.Mvc;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class DashboardController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}