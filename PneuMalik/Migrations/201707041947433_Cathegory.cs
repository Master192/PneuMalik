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
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Pattern = c.String(),
                        Design = c.String(),
                        IndexLi = c.Int(nullable: false),
                        SerieWidth = c.Int(nullable: false),
                        Construction = c.String(),
                        IndexSi = c.String(),
                        HighPr = c.Int(nullable: false),
                        Model = c.String(),
                        Size = c.String(),
                        Et = c.Int(nullable: false),
                        Holes = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Pitch = c.Int(nullable: false),
                        MiddleHole = c.Int(nullable: false),
                        DiscType_Id = c.Int(),
                        Manufacturer_Id = c.Int(),
                        Season_Id = c.Int(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiscTypes", t => t.DiscType_Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.VehicleTypes", t => t.Vehicle_Id)
                .Index(t => t.DiscType_Id)
                .Index(t => t.Manufacturer_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.DiscTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "Vehicle_Id", "dbo.VehicleTypes");
            DropForeignKey("dbo.Products", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Products", "Manufacturer_Id", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "DiscType_Id", "dbo.DiscTypes");
            DropForeignKey("dbo.Cathegories", "Product_Id", "dbo.Products");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Products", new[] { "Vehicle_Id" });
            DropIndex("dbo.Products", new[] { "Season_Id" });
            DropIndex("dbo.Products", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Products", new[] { "DiscType_Id" });
            DropIndex("dbo.Cathegories", new[] { "Product_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Seasons");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.DiscTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Cathegories");
        }
    }
}
