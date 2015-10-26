namespace Erp.Eam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_Info : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Infos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Category = c.Int(nullable: false),
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
            DropTable("dbo.Infos");
        }
    }
}
