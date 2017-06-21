namespace PneuMalik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cathegory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cathegories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Title = c.String(),
                        Default = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Keywords = c.String(),
                        Description = c.String(),
                        Annotation = c.String(),
                        Content = c.String(),
                        ItemsOnPage = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        ExternalUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cathegories");
        }
    }
}
