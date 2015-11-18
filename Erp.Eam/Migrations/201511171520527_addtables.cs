namespace Erp.Eam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockInDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockInId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StockIns", t => t.StockInId, cascadeDelete: true)
                .Index(t => t.StockInId);
            
            CreateTable(
                "dbo.StockIns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        CreatedBy = c.String(),
                        ModifyBy = c.String(),
                        Store = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockInDetails", "StockInId", "dbo.StockIns");
            DropIndex("dbo.StockInDetails", new[] { "StockInId" });
            DropTable("dbo.StockIns");
            DropTable("dbo.StockInDetails");
        }
    }
}
