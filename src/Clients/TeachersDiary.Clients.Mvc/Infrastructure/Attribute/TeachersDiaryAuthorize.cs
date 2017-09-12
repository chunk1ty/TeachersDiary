using System;
using System.Linq;
using System.Web.Mvc;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Attribute
{
    public class TeachersDiaryAuthorize : AuthorizeAttribute
    {
        public TeachersDiaryAuthorize(params ApplicationRoles[] roles)
        {
            var allowedRolesAsStrings = roles.Select(x => Enum.GetName(typeof(ApplicationRoles), x));

            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}