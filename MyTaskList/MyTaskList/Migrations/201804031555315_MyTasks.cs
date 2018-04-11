namespace MyTaskList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
