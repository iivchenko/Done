using System;

namespace Done.Web.Logging
{
    public sealed class LogEntry
    {
        public long Id { get; set; }

        public DateTime CreateDate { get; set; }

        public LogLevel Level { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }        

        public string Message { get; set; }
    }
}