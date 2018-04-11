using System.Data.Entity.Migrations;

namespace Done.Web.Migrations
{
    public partial class MyTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskItems", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskItems", "Description");
        }
    }
}
