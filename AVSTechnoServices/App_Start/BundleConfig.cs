using System.Web;
using System.Web.Optimization;

namespace AVSTechnoServices
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/nsadmincss").Include(
                      // <!-- Bootstrap CSS -->
                      "~/Content/Nscripts/css/bootstrap.min.css",
                      //  < !--bootstrap theme-- >
                      "~/Content/Nscripts/css/bootstrap.theme.css",
                      //< !--external css-- >
                      //< !--font icon-- >
                      "~/Content/Nscripts/css/elegant-icons-style.css",
                      "~/Content/Nscripts/css/font-awesome.min.css",
                      //< !--full calendar css-- >
                      "~/Content/Nscripts/assets/fullcalendar/fullcalendar/bootstrap-fullcalendar.css",
                      "~/Content/Nscripts/assets/fullcalendar/fullcalendar/fullcalendar.css",
                      //< !--Custom styles-- >
                      "~/Content/Nscripts/css/fullcalendar.css",
                      "~/Content/Nscripts/css/widgets.css",
                      "~/Content/Nscripts/css/jquery-ui-1.10.4.min.css",
                      "~/Content/Nscripts/css/Button.css",
                      "~/Content/Nscripts/css/TabbedSection.css",
                      "~/Content/Nscripts/css/normalize.css",
                       "~/Content/Nscripts/css/style.css",
                      "~/Content/Nscripts/css/style-responsive.css",
                       "~/Content/Custom.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/nsadminjs").Include(
                        //< !--javascripts-- >
                        "~/Content/Nscripts/js/jquery.js",
                        "~/Content/Nscripts/js/jquery-ui-1.10.4.min.js",
                        "~/Content/Nscripts/js/1.8.23/jquery-ui.min.js",
                        "~/Content/Nscripts/js/jquery/1.8.0/jquery.min.js",
                        "~/Content/Nscripts/js/jquery-1.8.3.min.js",
                        "~/Content/Nscripts/js/jquery.validate.min.js",
                        "~/Content/Nscripts/js/jquery-ui-1.9.2.custom.min.js",
                        "~/Content/Nscripts/js/form-validation-script.js",
                        //<!-- bootstrap -->
                        "~/Content/Nscripts/js/bootstrap.min.js",
                        //< !--jQuery full calendar-- >
                        "~/Content/Nscripts/js/fullcalendar.min.js",
                        "~/Content/Nscripts/assets/fullcalendar/fullcalendar/fullcalendar.js",
                        "~/Content/Nscripts/js/calendar-custom.js",
                        "~/Content/Nscripts/js/jquery.customSelect.min.js",
                        "~/Content/Nscripts/js/scripts.js",
                        "~/Content/Nscripts/js/jquery.autosize.min.js"
                    ));

            //< !--Casecade-- >
            bundles.Add(new ScriptBundle("~/Content/highchartCss").Include(
                        "~/Content/Charts/HighChartCss.css"
                    ));

            //< !--javascripts-- >
            bundles.Add(new ScriptBundle("~/bundles/highchartjs").Include(
                        "~/Content/Charts/highcharts.js",
                        "~/Content/Charts/exporting.js",
                        "~/Content/Charts/export-data.js",
                        "~/Content/Charts/accessibility.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/Notificationsjs").Include(
                       "~/Scripts/Notifications/bootstrapNotify.min.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                       "~/Scripts/_Custom.js"
                   ));
        }
    }
}
