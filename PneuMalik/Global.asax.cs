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

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
