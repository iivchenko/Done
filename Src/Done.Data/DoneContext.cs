using Done.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Done.Data
{
    // TODO: Provide new db initializer with starter kit: welcom task, basic tags (status.inprogress status.done, priority.low, priority.middle) etc.
    public sealed class DoneContext : IdentityDbContext<IdentityUser>
    {
        public DoneContext(DbContextOptions<DoneContext> options)
            : base(options)
        {            
        }

        public DbSet<Goal> Goals { get; set; }
    }
}