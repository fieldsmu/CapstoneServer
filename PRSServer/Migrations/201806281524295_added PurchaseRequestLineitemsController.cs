namespace PRSServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPurchaseRequestLineitemsController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseRequestLineitems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 80),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PurchaseRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequestId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequestLineitems", "PurchaseRequestId", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequestLineitems", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchaseRequestLineitems", new[] { "PurchaseRequestId" });
            DropIndex("dbo.PurchaseRequestLineitems", new[] { "ProductId" });
            DropTable("dbo.PurchaseRequestLineitems");
        }
    }
}
