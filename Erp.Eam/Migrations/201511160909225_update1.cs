namespace Erp.Eam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Color", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Color", c => c.String(nullable: false));
        }
    }
}
