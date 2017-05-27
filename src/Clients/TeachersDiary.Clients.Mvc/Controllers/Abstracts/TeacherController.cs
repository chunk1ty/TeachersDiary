using System.Web.Mvc;

using TeachersDiary.Common.Constants;

namespace TeachersDiary.Clients.Mvc.Controllers.Abstracts
{
    [Authorize(Roles = ApplicationRole.Teacher)]
    public abstract class TeacherController : BaseController
    {   
    }
}