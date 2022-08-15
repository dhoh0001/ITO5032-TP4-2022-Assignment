namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration64 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AlterColumn("dbo.AppUsers", "first_name", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "last_name", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "password", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address1", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address2", c => c.String(nullable: false));
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int());
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "Service_Provider_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int());
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int(nullable: false));
            AlterColumn("dbo.AppUsers", "address2", c => c.String());
            AlterColumn("dbo.AppUsers", "address1", c => c.String());
            AlterColumn("dbo.AppUsers", "password", c => c.String());
            AlterColumn("dbo.AppUsers", "last_name", c => c.String());
            AlterColumn("dbo.AppUsers", "first_name", c => c.String());
            CreateIndex("dbo.Ratings", "Service_Provider_id");
        }
    }
}
