namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "first_name", c => c.String());
            AlterColumn("dbo.AppUsers", "last_name", c => c.String());
            AlterColumn("dbo.AppUsers", "username", c => c.String());
            AlterColumn("dbo.AppUsers", "password", c => c.String());
            AlterColumn("dbo.AppUsers", "address1", c => c.String());
            AlterColumn("dbo.AppUsers", "address2", c => c.String());
            AlterColumn("dbo.AppUsers", "email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "email", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address2", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "address1", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "password", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "username", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "last_name", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "first_name", c => c.String(nullable: false));
        }
    }
}
