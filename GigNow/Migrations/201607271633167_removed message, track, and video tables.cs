namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedmessagetrackandvideotables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Videos", "Artist_ArtistId", "dbo.Artists");
            DropIndex("dbo.Tracks", new[] { "Artist_ArtistId" });
            DropIndex("dbo.Videos", new[] { "Artist_ArtistId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Tracks");
            DropTable("dbo.Videos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                        Artist_ArtistId = c.Int(),
                    })
                .PrimaryKey(t => t.VideoId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                        Artist_ArtistId = c.Int(),
                    })
                .PrimaryKey(t => t.TrackId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Content = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateIndex("dbo.Videos", "Artist_ArtistId");
            CreateIndex("dbo.Tracks", "Artist_ArtistId");
            AddForeignKey("dbo.Videos", "Artist_ArtistId", "dbo.Artists", "ArtistId");
            AddForeignKey("dbo.Tracks", "Artist_ArtistId", "dbo.Artists", "ArtistId");
        }
    }
}
