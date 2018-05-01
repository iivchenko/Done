using Done.Web.Filters;
using Done.Web.Logging;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Done.Web
{
    public class FilterConfig
    {
        public static void RegisterFilterProviders(FilterProviderCollection providers)
        {
            providers.Add(new LoggerFilterProvider { LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), ConfigurationManager.AppSettings["LogLevel"]) } );
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogExceptionFilterAttribute());
            filters.Add(new HandleErrorAttribute());            
        }
    }
}
