using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ninject;
using TeachersDiary.Clients.Mvc.Infrastructure.Session;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Attribute
{
    public class TeachersDiaryAuthorize : AuthorizeAttribute
    {
        [Inject]
        public ISessionStateService SessionState { get; set; }

        public TeachersDiaryAuthorize(params ApplicationRoles[] roles)
        {
            var allowedRolesAsStrings = roles.Select(x => Enum.GetName(typeof(ApplicationRoles), x));

            Roles = string.Join(",", allowedRolesAsStrings);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            SessionState.SyncSession(httpContext);

            return base.AuthorizeCore(httpContext);
        }
    }
}