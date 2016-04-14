using System.Web.Mvc;
using System.Web.Routing;

namespace StarProgrammerExtensionLibrary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{*anything}",
            //    defaults: new {controller = "Home", action = "Index"}
            //    );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new {controller = "Home", action = "Index"}
                );

            routes.MapRoute(
                name: "About",
                url: "About",
                defaults: new {controller = "Home", action = "About"}
                );

            routes.IgnoreRoute("Extensions/Home/*");

            routes.MapRoute(
                name: "Default",
                url: "Extensions/{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}