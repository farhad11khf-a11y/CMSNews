using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CMSNews.App_Start
{
    public class BundleConfig

    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            // CSS
            bundles.Add(new StyleBundle("~/Content/MyStyle")
                .Include(
                    "~/Content/bootstrap.min.css",
                    "~/Content/bootstrap.rtl.min.css",
                    "~/Content/Site.css"
                ));

            // jQuery اصلی
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                    "~/Scripts/jquery-3.7.1.min.js"
                ));

            // Bootstrap و سایر JavaScriptها
            bundles.Add(new ScriptBundle("~/Bundle/MyScript")
                .Include(
                    "~/Scripts/bootstrap.bundle.min.js",
                     "~/Scripts/myScripts.js"
                ));

            // اعتبارسنجی فرم‌ها برای Scaffold
            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include(
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js"
                ));

            //// CSS
            //bundles.Add(new StyleBundle("~/Content/MyStyle")
            //    .Include(
            //        "~/Content/bootstrap.min.css", "~/Content/bootstrap.rtl.min.css",
            //        "~/Content/Site.css"
            //    ));

            //// JavaScript
            //bundles.Add(new ScriptBundle("~/Bundle/MyScript")
            //    .Include(
            //        "~/Scripts/jquery-3.7.1.min.js",
            //        "~/Scripts/bootstrap.bundle.min.js"
            //    ));

            BundleTable.EnableOptimizations = false;
        }

        //public static void RegisterBundle(BundleCollection bundles)
        //{
        //    bundles.Add(new StyleBundle("~/Bundle/MyStyle")
        //        .Include("~/Content/bootstrap.min.css"
        //        , "~/Content/mySite.css", "~/Content/Slider.css"));

        //    bundles.Add(new Bundle("~/Bundle/MyScript")
        //        .Include("~/Scripts/bootstrap.bundle.min.js", "~/Scripts/myScipts.js"));
        //    bundles.Add(new ScriptBundle("~/Bundle/jquery")
        //       .Include("~/Scripts/jquery-3.7.1.min.js"));


        //}
    }
}