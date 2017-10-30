using System.Web.Mvc;
using TeachersDiary.Clients.Mvc.Infrastructure.Attribute;

namespace TeachersDiary.Clients.Mvc.Controllers.Abstracts
{
    [TeachersDiaryAuthorize]
    public abstract class BaseAuthorizeController : Controller
    {   
    }
}