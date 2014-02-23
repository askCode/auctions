namespace Bids.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        OwnerID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        AuctionEndDate = c.DateTime(nullable: false),
                        Owner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.UserProfile", t => t.Owner_UserId)
                .Index(t => t.Owner_UserId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Bid",
                c => new
                    {
                        BidID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        DatePlaced = c.DateTime(nullable: false),
                        BidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BidID)
                .ForeignKey("dbo.UserProfile", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Bid", new[] { "ItemID" });
            DropIndex("dbo.Bid", new[] { "UserID" });
            DropIndex("dbo.Item", new[] { "Owner_UserId" });
            DropForeignKey("dbo.Bid", "ItemID", "dbo.Item");
            DropForeignKey("dbo.Bid", "UserID", "dbo.UserProfile");
            DropForeignKey("dbo.Item", "Owner_UserId", "dbo.UserProfile");
            DropTable("dbo.Bid");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Item");
        }
    }
}
