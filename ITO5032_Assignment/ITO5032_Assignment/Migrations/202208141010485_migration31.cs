namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "average_rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "average_rating");
        }
    }
}
