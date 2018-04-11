namespace Done.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyTaskListToGoals : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaskItems", "Status");
            AddColumn("dbo.TaskItems", "State", c => c.Int(nullable: false));
            RenameTable("dbo.TaskItems", "Goals");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goals", "State");
            AddColumn("dbo.Goals", "Status", c => c.Int(nullable: false));
            RenameTable("dbo.Goals", "TaskItems");
        }
    }
}
