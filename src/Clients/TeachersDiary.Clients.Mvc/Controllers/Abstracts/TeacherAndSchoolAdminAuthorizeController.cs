using TeachersDiary.Clients.Mvc.Infrastructure.Attribute;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Clients.Mvc.Controllers.Abstracts
{
    [TeachersDiaryAuthorize(ApplicationRoles.Teacher, ApplicationRoles.SchoolAdministrator)]
    public abstract class TeacherAndSchoolAdminAuthorizeController : BaseAuthorizeController
    {   
    }
}