namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "username", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "password", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address1", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address2", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "description", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "available_day", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "available_start_time", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "available_end_time", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "max_occupancy", c => c.String(nullable: false));
            AlterColumn("dbo.Bookables", "booking_type", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "address1", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "address2", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "room", c => c.String(nullable: false));
            AlterColumn("dbo.Notifications", "message", c => c.String(nullable: false));
            AlterColumn("dbo.Ratings", "comment", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "file_name", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "file_location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "file_location", c => c.String());
            AlterColumn("dbo.Files", "file_name", c => c.String());
            AlterColumn("dbo.Ratings", "comment", c => c.String());
            AlterColumn("dbo.Notifications", "message", c => c.String());
            AlterColumn("dbo.Locations", "room", c => c.String());
            AlterColumn("dbo.Locations", "address2", c => c.String());
            AlterColumn("dbo.Locations", "address1", c => c.String());
            AlterColumn("dbo.Bookables", "booking_type", c => c.String());
            AlterColumn("dbo.Bookables", "max_occupancy", c => c.String());
            AlterColumn("dbo.Bookables", "available_end_time", c => c.String());
            AlterColumn("dbo.Bookables", "available_start_time", c => c.String());
            AlterColumn("dbo.Bookables", "available_day", c => c.String());
            AlterColumn("dbo.Bookables", "description", c => c.String());
            AlterColumn("dbo.Bookables", "name", c => c.String());
            AlterColumn("dbo.AppUsers", "external_id", c => c.String());
            AlterColumn("dbo.AppUsers", "email", c => c.String());
            AlterColumn("dbo.AppUsers", "address2", c => c.String());
            AlterColumn("dbo.AppUsers", "address1", c => c.String());
            AlterColumn("dbo.AppUsers", "salt", c => c.String());
            AlterColumn("dbo.AppUsers", "password", c => c.String());
            AlterColumn("dbo.AppUsers", "username", c => c.String());
        }
    }
}
