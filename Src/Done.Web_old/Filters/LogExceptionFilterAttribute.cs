using System.Web.Mvc;
using Done.Web.Logging;

namespace Done.Web.Filters
{
    public sealed class LogExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            new Logger(LogLevel.Error)
                .Log(
                LogLevel.Error,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString(),
                filterContext.Exception.ToString());                
        }
    }
}