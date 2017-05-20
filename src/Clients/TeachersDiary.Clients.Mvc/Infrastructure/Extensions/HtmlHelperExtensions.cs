using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            var result = controller == currentController && action == currentAction ? cssClass : String.Empty;

            return result;
        }
    }
}