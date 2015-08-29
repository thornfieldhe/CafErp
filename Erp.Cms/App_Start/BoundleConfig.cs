namespace Erp.Cms
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/assets/basiccss").Include(
          "~/assets/css/bootstrap.min.css",
            "~/assets/css/font-awesome.min.css",
          "~/Content/weather-icons.min.css"));

            bundles.Add(bundle: new StyleBundle("~/assets/beyondcss").Include(
                      "~/assets/css/beyond.min.css",
                      "~/assets/css/demo.min.css",
                      "~/assets/css/typicons.min.css",
                      "~/assets/css/animate.min.css",
                      "~/assets/css/dataTables.bootstrap.css"
                      ));


            bundles.Add(new ScriptBundle("~/assets/basicjs").Include(
                      "~/assets/js/jquery.min.js",
                      "~/assets/js/bootstrap.min.js",
                       "~/assets/js/slimscroll/jquery.slimscroll.min.js",
                       "~/assets/js/lodash.min.js",
                      "~/assets/js/beyond.js",
                        //                       "~/assets/js/datatable/jquery.dataTables.min.js",
                        //                        "~/assets/js/services/dataTables.tableTools.min.js",
                        //                        "~/assets/js/services/dataTables.bootstrap.min.js",
                        "~/assets/js/bootbox/bootbox.js",
                         "~/assets/js/lodash.min.js",
                     "~/assets/js/juicer-min.js",
                     "~/assets/js/validation/bootstrapValidator.js"));

            bundles.Add(new ScriptBundle("~/assets/angular").Include(
                      "~/assets/js/angular/angular.min.js",
                      "~/assets/js/angular/angular-messages.min.js"));

            bundles.Add(new ScriptBundle("~/assets/app").Include(
                     "~/assets/js/app/index.js",
                     "~/assets/js/app/base.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}