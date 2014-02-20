namespace Bids.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemTitleAndDateRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Item", "Title", c => c.String());
        }
    }
}
