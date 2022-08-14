namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "average_rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "average_rating", c => c.Int(nullable: false));
        }
    }
}
