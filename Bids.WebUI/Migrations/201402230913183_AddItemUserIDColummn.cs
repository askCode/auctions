namespace Bids.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemUserIDColummn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Owner_UserId", "dbo.UserProfile");
            DropIndex("dbo.Item", new[] { "Owner_UserId" });
            RenameColumn(table: "dbo.Item", name: "Owner_UserId", newName: "UserId");
            AddForeignKey("dbo.Item", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Item", "UserId");
            DropColumn("dbo.Item", "OwnerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "OwnerID", c => c.Int(nullable: false));
            DropIndex("dbo.Item", new[] { "UserId" });
            DropForeignKey("dbo.Item", "UserId", "dbo.UserProfile");
            RenameColumn(table: "dbo.Item", name: "UserId", newName: "Owner_UserId");
            CreateIndex("dbo.Item", "Owner_UserId");
            AddForeignKey("dbo.Item", "Owner_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
