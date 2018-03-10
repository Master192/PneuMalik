using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PneuMalik
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var routes = RouteTable.Routes;

            routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Kategorie",
                url: "Kategorie/{title}",
                defaults: new { controller = "Kategorie", action = "Index", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Pneumatiky",
                url: "pneumatiky/{title}",
                defaults: new { controller = "Pneumatiky", action = "Index", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Disky",
                url: "disky/{title}",
                defaults: new { controller = "Disky", action = "Index", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Ocelovedisky",
                url: "ocelovediskydisky/{title}",
                defaults: new { controller = "Ocelovedisky", action = "Index", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Hlinikovedisky",
                url: "hlinikovediskydisky/{title}",
                defaults: new { controller = "Hlinikovedisky", action = "Index", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Prislusenstvi",
                url: "prislusenstvi/{title}",
                defaults: new { controller = "Prislusenstvi", action = "Index", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Pneumatika",
                url: "pneumatika/{id}/{suffix}",
                defaults: new { controller = "Pneumatiky", action = "Detail", title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Disk",
                url: "disk/{id}/{suffix}",
                defaults: new { controller = "Disky", action = "Detail", title = UrlParameter.Optional }
            );

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                return;
            }

            var ErrorInfo = Server.GetLastError().GetBaseException();

            var strError = $"\n\nCas vygenerovani chyby: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";

            strError += "\n\nText chyby: " + ErrorInfo.Message +
                "\nPocatek trasovani chyby: " + ErrorInfo.TargetSite +
                "\nQueryString:\n";

            for (int i = 0; i < Context.Request.QueryString.Count; i++)
            {
                strError += Context.Request.QueryString.Keys[i] + ": " + Context.Request.QueryString[i] + "\n";
            }

            strError += "Post Data:\n";
            for (int i = 0; i < Context.Request.Form.Count; i++)
                strError += Context.Request.Form.Keys[i] + ": " + Context.Request.Form[i] + "\n";

            // Gathering Server Variables information
            for (int i = 0; i < Context.Request.ServerVariables.Count; i++)
            {
                string strKey = Context.Request.ServerVariables.Keys[i];
                if (strKey == "HTTP_HOST" || strKey == "HTTP_REFERER" || strKey == "PATH_TRANSLATED" || strKey == "HTTP_COOKIE")
                {
                    strError += strKey + ": " + Context.Request.ServerVariables[i] + "\n";
                }
            }
            strError += "------------------------------------------------";

            // zápis do souboru // *** //
            File.AppendAllText(Path.Combine(ConfigurationManager.AppSettings["ErrorLog"], "errorLog.txt"), strError);

            Response.Redirect("/Eshop/Error");
        }
    }
}
