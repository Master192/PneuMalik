namespace PneuMalik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cathegory1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "VehicleType_Id", c => c.Int());
            CreateIndex("dbo.Products", "VehicleType_Id");
            AddForeignKey("dbo.Products", "VehicleType_Id", "dbo.VehicleTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "VehicleType_Id", "dbo.VehicleTypes");
            DropIndex("dbo.Products", new[] { "VehicleType_Id" });
            DropColumn("dbo.Products", "VehicleType_Id");
        }
    }
}
