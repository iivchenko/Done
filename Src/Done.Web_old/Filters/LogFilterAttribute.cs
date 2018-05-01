using System.Diagnostics;
using System.Web.Mvc;
using Done.Web.Logging;

namespace Done.Web.Filters
{
    public sealed class LogFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly Stopwatch _stopwatch;
        private readonly Logger _logger;

        public LogFilterAttribute(LogLevel level)
        {
            _stopwatch = new Stopwatch();
            _logger = new Logger(level);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopwatch.Stop();

            _logger
                .Log(
                    LogLevel.Trace,
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName,
                    $"Execution duration = { _stopwatch.Elapsed}.");

            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopwatch.Start();

            base.OnActionExecuting(filterContext);
        } 
    }
}