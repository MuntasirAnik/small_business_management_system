namespace Brainstorm.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Full : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                        LoyaltyPoint = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                        ReorderLevel = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PurchaseProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        ProductCode = c.String(),
                        MfgDate = c.DateTime(nullable: false, storeType: "date"),
                        ExpDate = c.DateTime(nullable: false, storeType: "date"),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        MRPPrice = c.Double(nullable: false),
                        Remarks = c.String(),
                        Purchase_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.Purchase_Id)
                .Index(t => t.Purchase_Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        BillNo = c.String(),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                        ContactPerson = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        MRP = c.Double(nullable: false),
                        TotalMRP = c.Double(nullable: false),
                        Sale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sales", t => t.Sale_Id)
                .Index(t => t.Sale_Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleProducts", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseProducts", "Purchase_Id", "dbo.Purchases");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleProducts", new[] { "Sale_Id" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseProducts", new[] { "Purchase_Id" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Sales");
            DropTable("dbo.SaleProducts");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
        }
    }
}
