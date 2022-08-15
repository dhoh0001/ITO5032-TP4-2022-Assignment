namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration65 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AlterColumn("dbo.AppUsers", "address1", c => c.String());
            AlterColumn("dbo.AppUsers", "address2", c => c.String());
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int());
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "Service_Provider_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int());
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int(nullable: false));
            AlterColumn("dbo.AppUsers", "address2", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address1", c => c.String(nullable: false));
            CreateIndex("dbo.Ratings", "Service_Provider_id");
        }
    }
}
