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

            var exception = Server.GetLastError();
           
            // logování chyb sem

            Response.Redirect("/");
        }
    }
}
