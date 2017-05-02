using System.Diagnostics.CodeAnalysis;
using System.Web.Optimization;

namespace TeachersDiary.Clients.Mvc
{
    [ExcludeFromCodeCoverage]
    public class BundleConfig
    {
        private const string JqueryDataTableCssCdn = "https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css";
        private const string BootstrapCssCdn = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css";

        private const string BootstrapJsCdn = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";
        private const string JqueryDataTableJsCdn = "https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js";
        private const string JqueryCdn = "https://code.jquery.com/jquery-1.12.4.min.js";
        private const string JqueryValidateCdn = "https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.16.0/jquery.validate.min.js";
        private const string ModernizrJsCdn = "https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";


        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            RegisterScripts(bundles);
            RegisterStyles(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/teacherDiary").Include(
                "~/Content/teacherDiary.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap", BootstrapCssCdn).Include(
                "~/Content/bootstrap.css"));
            
            bundles.Add(new StyleBundle("~/Content/jqueryDataTable", JqueryDataTableCssCdn)
                .Include("~/Content/DataTables/css/jquery.dataTables.min.css"));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery", JqueryCdn).Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval", JqueryValidateCdn).Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap", BootstrapJsCdn).Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));
         
            bundles.Add(new ScriptBundle("~/bundles/modernizr", ModernizrJsCdn).Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryDataTable", JqueryDataTableJsCdn)
                .Include("~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/respond")
                .Include("~/Scripts/respond.js"));
        }
    }
}
