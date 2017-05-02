using System;
using System.Linq;
using System.Threading;
using System.Web;

namespace TeachersDiary.Clients.Mvc.Infrastructure.HttpModules
{
    public class CultureModule : IHttpModule
    {
        private const string Bg = "bg";

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.Context_BeginRequest;
        }

        public void Dispose()
        {
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            var urlParts = HttpContext.Current.Request.Url.AbsoluteUri.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlParts.Count() > 2)
            {
                string lang = urlParts[2];

                if (lang == Bg)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Bg);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Bg);
                }
            }
        }
    }
}