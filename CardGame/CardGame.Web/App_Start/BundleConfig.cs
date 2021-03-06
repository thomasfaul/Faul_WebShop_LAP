﻿using System.Web.Optimization;

namespace CardGame.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/toastr.js",
                      "~/Scripts/myscript.js"));
            bundles.Add(new ScriptBundle("~/bundles/d3").Include(
                       "~/Scripts/d3/d3.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui") .Include(
                "~/Content/themes/base/*.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/Style.css"));
        }
    }
}