using System.Web.Mvc;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class LandingController : NonAuthorizeController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}