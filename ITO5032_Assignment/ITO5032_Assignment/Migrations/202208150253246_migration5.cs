namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "external_id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "external_id", c => c.String(nullable: false));
        }
    }
}
