using Done.Web.Logging;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Done.Web.Filters
{
    public class LoggerFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return new[] { new Filter(new LogFilterAttribute(LogLevel), FilterScope.Global, 0) };
        }

        public LogLevel LogLevel { get; set; }
    }
}