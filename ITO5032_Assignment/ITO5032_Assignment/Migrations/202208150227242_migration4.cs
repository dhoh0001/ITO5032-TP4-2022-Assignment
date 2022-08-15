namespace ITO5032_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "salt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "salt", c => c.String(nullable: false));
        }
    }
}
