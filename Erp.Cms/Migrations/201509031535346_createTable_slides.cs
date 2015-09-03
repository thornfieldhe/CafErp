namespace Erp.Cms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable_slides : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                        Rate = c.Int(nullable: false),
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
            DropTable("dbo.Slides");
        }
    }
}
