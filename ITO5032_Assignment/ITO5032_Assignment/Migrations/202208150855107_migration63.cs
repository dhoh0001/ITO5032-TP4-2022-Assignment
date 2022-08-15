namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration63 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "User_id1", "dbo.AppUsers");
            AddColumn("dbo.Ratings", "AppUser_id", c => c.Int());
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int());
            CreateIndex("dbo.Ratings", "Service_Provider_id");
            CreateIndex("dbo.Ratings", "AppUser_id");
            AddForeignKey("dbo.Ratings", "AppUser_id", "dbo.AppUsers", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "AppUser_id", "dbo.AppUsers");
            DropForeignKey("dbo.Ratings", "Service_Provider_id", "dbo.AppUsers");
            DropIndex("dbo.Ratings", new[] { "AppUser_id" });
            DropIndex("dbo.Ratings", new[] { "Service_Provider_id" });
            AlterColumn("dbo.Ratings", "Service_Provider_id", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "AppUser_id");
            AddForeignKey("dbo.Ratings", "User_id1", "dbo.AppUsers", "id");
        }
    }
}
