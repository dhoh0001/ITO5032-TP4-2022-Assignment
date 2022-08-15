namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration62 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "file_id1", c => c.Int());
            CreateIndex("dbo.Locations", "file_id1");
            AddForeignKey("dbo.Locations", "file_id1", "dbo.Files", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "file_id1", "dbo.Files");
            DropIndex("dbo.Locations", new[] { "file_id1" });
            DropColumn("dbo.Locations", "file_id1");
        }
    }
}
