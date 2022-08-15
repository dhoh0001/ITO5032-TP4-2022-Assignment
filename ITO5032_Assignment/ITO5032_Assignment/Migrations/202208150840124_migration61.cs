namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "file_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "file_id");
        }
    }
}
