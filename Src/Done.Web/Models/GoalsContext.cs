using System.Data.Entity;

namespace Done.Web.Models
{
    public sealed class GoalsContext : DbContext
    {
        public GoalsContext()
            : base("Default")
        {
        }

        public DbSet<Goal> Goals { get; set; }
    }
}