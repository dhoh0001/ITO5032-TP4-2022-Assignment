namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        role_id = c.String(),
                        first_name = c.String(),
                        last_name = c.String(),
                        date_of_birth = c.String(),
                        username = c.String(),
                        password = c.String(),
                        salt = c.String(),
                        address1 = c.String(),
                        address2 = c.String(),
                        email = c.String(),
                        external_id = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        start_datetime = c.String(),
                        end_datetime = c.String(),
                        User_id = c.Int(nullable: false),
                        Bookable_id = c.Int(nullable: false),
                        Bookable_id1 = c.Int(),
                        User_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Bookables", t => t.Bookable_id1)
                .ForeignKey("dbo.AppUsers", t => t.User_id1)
                .Index(t => t.Bookable_id1)
                .Index(t => t.User_id1);
            
            CreateTable(
                "dbo.Bookables",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        available_day = c.String(),
                        available_start_time = c.String(),
                        available_end_time = c.String(),
                        max_occupancy = c.String(),
                        booking_type = c.String(),
                        Location_id = c.Int(nullable: false),
                        Location_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Locations", t => t.Location_id1)
                .Index(t => t.Location_id1);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        address1 = c.String(),
                        address2 = c.String(),
                        room = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        notification_datetime = c.String(),
                        User_id = c.Int(nullable: false),
                        User_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AppUsers", t => t.User_id1)
                .Index(t => t.User_id1);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        score = c.String(),
                        comment = c.String(),
                        service_provider_id = c.String(),
                        User_id = c.Int(nullable: false),
                        User_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AppUsers", t => t.User_id1)
                .Index(t => t.User_id1);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        file_name = c.String(),
                        file_location = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ratings", "User_id1", "dbo.AppUsers");
            DropForeignKey("dbo.Notifications", "User_id1", "dbo.AppUsers");
            DropForeignKey("dbo.Bookings", "User_id1", "dbo.AppUsers");
            DropForeignKey("dbo.Bookables", "Location_id1", "dbo.Locations");
            DropForeignKey("dbo.Bookings", "Bookable_id1", "dbo.Bookables");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ratings", new[] { "User_id1" });
            DropIndex("dbo.Notifications", new[] { "User_id1" });
            DropIndex("dbo.Bookables", new[] { "Location_id1" });
            DropIndex("dbo.Bookings", new[] { "User_id1" });
            DropIndex("dbo.Bookings", new[] { "Bookable_id1" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Files");
            DropTable("dbo.Ratings");
            DropTable("dbo.Notifications");
            DropTable("dbo.Locations");
            DropTable("dbo.Bookables");
            DropTable("dbo.Bookings");
            DropTable("dbo.AppUsers");
        }
    }
}
