namespace Erp.Eam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockInDetails", "Store", c => c.String());
            DropColumn("dbo.StockIns", "Store");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockIns", "Store", c => c.String());
            DropColumn("dbo.StockInDetails", "Store");
        }
    }
}
