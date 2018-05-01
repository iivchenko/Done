using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Done.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // TODO: Provide some default values during new db creation. Some sample goals etc.
            //Database.SetInitializer(new GoalsInitializer());           

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterFilterProviders(FilterProviders.Providers);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
