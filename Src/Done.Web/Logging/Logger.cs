using System;
using System.Threading;

namespace Done.Web.Logging
{
    public sealed class Logger
    {
        private static AsyncLocal<LoggingContext> LoggingContext = new AsyncLocal<LoggingContext>();
        private readonly LogLevel _level;

        public Logger(LogLevel level)
        {
            LoggingContext.Value = new LoggingContext();

            _level = level;
        }       
        
        public void Log(LogLevel level, string controller, string action, string message)
        {
            LogInternal(level, controller, action, message);
        }       

        private void LogInternal(LogLevel level, string controller, string action, string message)
        {   
            if (level >= _level)
            {
                LoggingContext
               .Value
               .LogEntries
               .Add(new LogEntry
               {
                   Level = level,
                   CreateDate = DateTime.UtcNow,
                   Controller = controller,
                   Action = action,
                   Message = message
               });

                LoggingContext.Value.SaveChanges();
            }           
        }
    }
}