namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedartistIdtoslot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slots", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Slots", "ArtistId");
            AddForeignKey("dbo.Slots", "ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
            DropColumn("dbo.Slots", "IsFilled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slots", "IsFilled", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Slots", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Slots", new[] { "ArtistId" });
            DropColumn("dbo.Slots", "ArtistId");
        }
    }
}
