using System.Threading.Tasks;
using System.Web.Mvc;

using TeachersDiary.Clients.Mvc.Controllers.Abstracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class DashboardController : BaseAuthorizeController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}