namespace Todo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedTaskModelfieldtomakerequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskModels", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskModels", "Title", c => c.String());
        }
    }
}
