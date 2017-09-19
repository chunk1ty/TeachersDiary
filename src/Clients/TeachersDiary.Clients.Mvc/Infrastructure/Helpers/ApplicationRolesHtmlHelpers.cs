using System;
using System.Web.Mvc;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Helpers
{
    public static class ApplicationRolesHtmlHelpers
    {
        public static string DisplayApplicationRole(this HtmlHelper html, ApplicationRoles applicationRoles)
        {
            switch (applicationRoles)
            {
                case ApplicationRoles.None:
                    return "Няма";
                case ApplicationRoles.Teacher:
                    return "Учител";
                default:
                    throw new InvalidOperationException();
            }  
        }
    }
}