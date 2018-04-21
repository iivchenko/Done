using System.Data.Entity;

namespace Done.Web.Logging
{
    public sealed class LoggingContext : DbContext
    {
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}