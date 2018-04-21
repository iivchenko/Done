using System.Data.Entity;

namespace Done.Web.Models.Data
{
    public sealed class GoalsContext : DbContext
    {
        public GoalsContext()
            : base("Done")
        {
        }

        public DbSet<Goal> Goals { get; set; }
    }
}