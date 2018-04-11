using System.Data.Entity.Migrations;
using Done.Web.Models;

namespace Done.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GoalsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Done.Models.GoalsContext";
        }

        protected override void Seed(GoalsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
