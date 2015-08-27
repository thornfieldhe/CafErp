namespace Erp.Cms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_Article : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Order = c.Int(nullable: false),
                        ParentId = c.Guid(),
                        Level = c.Int(nullable: false),
                        LevelCode = c.String(),
                        Category = c.Int(nullable: false),
                        Content = c.String(),
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
            DropTable("dbo.Articles");
        }
    }
}
