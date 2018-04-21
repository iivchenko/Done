using System;

namespace Done.Web.Logging
{
    public sealed class LogEntry
    {
        public long Id { get; set; }

        public LogLevel Level { get; set; }

        public DateTime CreateDate { get; set; }

        public string Message { get; set; }
    }
}