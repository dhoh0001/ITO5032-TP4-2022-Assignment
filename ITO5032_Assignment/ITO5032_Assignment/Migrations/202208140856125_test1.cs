namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "first_name", c => c.String(nullable: false));
            AlterColumn("dbo.AppUsers", "last_name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "last_name", c => c.String());
            AlterColumn("dbo.AppUsers", "first_name", c => c.String());
        }
    }
}
