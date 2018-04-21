using System;

namespace Done.Web.Logging
{
    public static class Logger
    {
        private static LoggingContext LoggingContext;

        static Logger()
        {
            LoggingContext = new LoggingContext();
        }

        public static void Log(LogLevel level, string message)
        {
            LogInternal(level, message);
        }

        public static void LogTrace(string message)
        {
            LogInternal(LogLevel.Trace, message);
        }

        public static void LogTrace(Exception exception)
        {
            LogInternal(LogLevel.Trace, exception.ToString());
        }

        public static void LogDebug(string message)
        {
            LogInternal(LogLevel.Debug, message);
        }

        public static void LogDebug(Exception exception)
        {
            LogInternal(LogLevel.Debug, exception.ToString());
        }

        public static void LogInfo(string message)
        {
            LogInternal(LogLevel.Info, message);
        }

        public static void LogInfo(Exception exception)
        {
            LogInternal(LogLevel.Info, exception.ToString());
        }

        public static void LogWarning(string message)
        {
            LogInternal(LogLevel.Warning, message);
        }

        public static void LogWarning(Exception exception)
        {
            LogInternal(LogLevel.Warning, exception.ToString());
        }

        public static void LogError(string message)
        {
            LogInternal(LogLevel.Error, message);
        }

        public static void LogError(Exception exception)
        {
            LogInternal(LogLevel.Error, exception.ToString());
        }

        private static void LogInternal(LogLevel level, string message)
        {
            LoggingContext
                .LogEntries
                .Add(new LogEntry
                {
                    Level = level,
                    CreateDate = DateTime.UtcNow,
                    Message = message
                });

            LoggingContext.SaveChanges();
        }
    }
}