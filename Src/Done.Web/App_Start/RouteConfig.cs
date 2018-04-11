using System.Web.Mvc;
using System.Web.Routing;

namespace Done.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
              name: "task_search",
              url: "search/{pattern}",
              defaults: new { controller = "Goals", action = "Index" });

            routes.MapRoute(
              name: "task",
              url: "task/{id}",
              defaults: new { controller = "Goals", action = "Edit" });

            routes.MapRoute(
             name: "old_home",
             url: "Home/Index",
             defaults: new { controller = "Goals", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Goals", action = "Index", id = UrlParameter.Optional });
        }
    }
}
