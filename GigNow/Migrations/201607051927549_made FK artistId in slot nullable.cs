namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeFKartistIdinslotnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Slots", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Slots", new[] { "ArtistId" });
            AlterColumn("dbo.Slots", "ArtistId", c => c.Int());
            CreateIndex("dbo.Slots", "ArtistId");
            AddForeignKey("dbo.Slots", "ArtistId", "dbo.Artists", "ArtistId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slots", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Slots", new[] { "ArtistId" });
            AlterColumn("dbo.Slots", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Slots", "ArtistId");
            AddForeignKey("dbo.Slots", "ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
        }
    }
}
