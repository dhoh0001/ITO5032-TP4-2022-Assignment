namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration7 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AddColumn("dbo.Bookables", "service_provider_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int());
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "Service_Provider_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int());
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int(nullable: false));
            DropColumn("dbo.Bookables", "service_provider_id");
            CreateIndex("dbo.Ratings", "Service_Provider_id");
        }
    }
}
