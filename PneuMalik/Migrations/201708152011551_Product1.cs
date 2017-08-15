namespace PneuMalik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Sale", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Dph", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "FuelConsumption", c => c.String());
            AddColumn("dbo.Products", "Adhesion", c => c.String());
            AddColumn("dbo.Products", "NoiseLevelDb", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "NoiseLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "EfficiencyCathegory", c => c.String());
            AddColumn("dbo.Products", "Standard", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Standard");
            DropColumn("dbo.Products", "EfficiencyCathegory");
            DropColumn("dbo.Products", "NoiseLevel");
            DropColumn("dbo.Products", "NoiseLevelDb");
            DropColumn("dbo.Products", "Adhesion");
            DropColumn("dbo.Products", "FuelConsumption");
            DropColumn("dbo.Products", "Dph");
            DropColumn("dbo.Products", "Sale");
        }
    }
}
