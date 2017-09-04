namespace PneuMalik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        DeliveryTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PriceObjects");
        }
    }
}
