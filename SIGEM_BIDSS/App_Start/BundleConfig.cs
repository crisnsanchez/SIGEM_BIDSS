using System.Web;
using System.Web.Optimization;

namespace SIGEM_BIDSS
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Content/template/plugins/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/adminlte").Include(
                     "~/Content/template/dist/css/adminlte.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Content/template/plugins/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                        "~/Content/template/plugins/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/adminlte").Include(
                        "~/Content/template/dist/js/adminlte.min.js"));

        }
    }
}
