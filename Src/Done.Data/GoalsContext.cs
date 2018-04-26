using System.Data.Entity;
using Done.Domain;

namespace Done.Data
{
    public sealed class DoneContext : DbContext
    {
        public DoneContext(string name)
            : base(name)
        {            
        }

        public DbSet<Goal> Goals { get; set; }
    }
}