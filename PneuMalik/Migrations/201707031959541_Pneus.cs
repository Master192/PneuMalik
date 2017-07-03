namespace PneuMalik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pneus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pneus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pattern = c.String(),
                        Design = c.String(),
                        IndexLi = c.Int(nullable: false),
                        SerieWidth = c.Int(nullable: false),
                        Construction = c.String(),
                        IndexSi = c.String(),
                        HighPr = c.Int(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Tip = c.Boolean(nullable: false),
                        Action = c.Boolean(nullable: false),
                        InStock = c.Boolean(nullable: false),
                        Width = c.Int(nullable: false),
                        Diameter = c.Int(nullable: false),
                        Ean = c.String(),
                        PriceCommon = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                        Manufacturer_Id = c.Int(),
                        Season_Id = c.Int(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.VehicleTypes", t => t.Vehicle_Id)
                .Index(t => t.Manufacturer_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cathegories", "Pneu_Id", c => c.Int());
            CreateIndex("dbo.Cathegories", "Pneu_Id");
            AddForeignKey("dbo.Cathegories", "Pneu_Id", "dbo.Pneus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pneus", "Vehicle_Id", "dbo.VehicleTypes");
            DropForeignKey("dbo.Pneus", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Pneus", "Manufacturer_Id", "dbo.Manufacturers");
            DropForeignKey("dbo.Cathegories", "Pneu_Id", "dbo.Pneus");
            DropIndex("dbo.Pneus", new[] { "Vehicle_Id" });
            DropIndex("dbo.Pneus", new[] { "Season_Id" });
            DropIndex("dbo.Pneus", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Cathegories", new[] { "Pneu_Id" });
            DropColumn("dbo.Cathegories", "Pneu_Id");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Seasons");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Pneus");
        }
    }
}
