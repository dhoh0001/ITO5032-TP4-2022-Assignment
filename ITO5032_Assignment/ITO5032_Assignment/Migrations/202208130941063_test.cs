namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "role_id", c => c.Int(nullable: false));
            AlterColumn("dbo.AppUsers", "date_of_birth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "start_datetime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "end_datetime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookables", "available_start_time", c => c.String());
            AlterColumn("dbo.Notifications", "notification_datetime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Ratings", "score", c => c.Int(nullable: false));
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "service_provider_id", c => c.String());
            AlterColumn("dbo.Ratings", "score", c => c.String());
            AlterColumn("dbo.Notifications", "notification_datetime", c => c.String());
            AlterColumn("dbo.Bookables", "available_start_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "end_datetime", c => c.String());
            AlterColumn("dbo.Bookings", "start_datetime", c => c.String());
            AlterColumn("dbo.AppUsers", "date_of_birth", c => c.String());
            AlterColumn("dbo.AppUsers", "role_id", c => c.String());
        }
    }
}
