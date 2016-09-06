using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace StreamWebsite.MVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/vendor")
                .Include("~/Content/vendor/font-awesome.css", "~/Content/vendor/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/vendor")
                .Include("~/Scripts/vendor/modernizr-2.6.2.js", "~/Scripts/vendor/jquery-1.10.2.min.js", "~/Scripts/vendor/bootstrap.min.js"));
        }
    }
}