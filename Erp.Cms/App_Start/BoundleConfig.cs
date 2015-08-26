﻿namespace Erp.Cms
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
                      "~/assets/css/animate.min.css"));


            bundles.Add(new ScriptBundle("~/assets/basicjs").Include(
                      "~/assets/js/jquery.min.js",
                      "~/assets/js/bootstrap.min.js",
                       "~/assets/js/slimscroll/jquery.slimscroll.min.js",
                       "~/assets/js/lodash.min.js",
                      "~/assets/js/beyond.js"));


            bundles.Add(new ScriptBundle("~/assets/angular").Include(
                      "~/assets/js/angular.min.js",
                      "~/assets/js/angular-messages.min.js"));

            //                        bundles.Add(new ScriptBundle("~/assets/app").Include(
            //                                  "~/scripts/app/app.js",
            //                                  "~/scripts/app/services/manageService.js",
            //                                  "~/scripts/app/controllers/manageController.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}