namespace PneuMalik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Text : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Texts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Texts");
        }
    }
}
