namespace Bids.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        LoginName = c.String(),
                        ReputationPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberID);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AuctionEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Bid",
                c => new
                    {
                        BidID = c.Int(nullable: false, identity: true),
                        MemberID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        DatePlaced = c.DateTime(nullable: false),
                        BidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BidID)
                .ForeignKey("dbo.Member", t => t.MemberID, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.MemberID)
                .Index(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Bid", new[] { "ItemID" });
            DropIndex("dbo.Bid", new[] { "MemberID" });
            DropForeignKey("dbo.Bid", "ItemID", "dbo.Item");
            DropForeignKey("dbo.Bid", "MemberID", "dbo.Member");
            DropTable("dbo.Bid");
            DropTable("dbo.Item");
            DropTable("dbo.Member");
        }
    }
}
