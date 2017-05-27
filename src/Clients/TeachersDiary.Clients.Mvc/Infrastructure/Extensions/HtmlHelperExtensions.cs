using System.Web.Mvc;
using static System.String;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (IsNullOrEmpty(cssClass))
            { 
                cssClass = "active";
            }

            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (IsNullOrEmpty(controller))
            { 
                controller = currentController;
            }

            if (IsNullOrEmpty(action))
            { 
                action = currentAction;
            }

            var result = controller == currentController && action == currentAction ? cssClass : Empty;

            // TODO temporary solution 
            if (controller == "class" && currentController == "student")
            {
                result = cssClass;
            }

            return result;
        }
    }
}