using System.Data.Entity;

namespace Done.Web.Logging
{
    public sealed class LoggingContext : DbContext
    {
        static LoggingContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LoggingContext>());
        }

        public LoggingContext()
            : base("Logging")
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }
    }
}