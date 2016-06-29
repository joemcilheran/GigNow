namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedArtistAndVenue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artists", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Artists", "track1Id", "dbo.Tracks");
            DropForeignKey("dbo.Artists", "track2Id", "dbo.Tracks");
            DropForeignKey("dbo.Artists", "track3Id", "dbo.Tracks");
            DropForeignKey("dbo.Artists", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.Venues", "PhotoId", "dbo.Photos");
            DropIndex("dbo.Artists", new[] { "PhotoId" });
            DropIndex("dbo.Artists", new[] { "VideoId" });
            DropIndex("dbo.Artists", new[] { "track1Id" });
            DropIndex("dbo.Artists", new[] { "track2Id" });
            DropIndex("dbo.Artists", new[] { "track3Id" });
            DropIndex("dbo.Venues", new[] { "PhotoId" });
            AddColumn("dbo.Photos", "ArtistId", c => c.Int());
            AddColumn("dbo.Photos", "VenueId", c => c.Int());
            AddColumn("dbo.Tracks", "ArtistId", c => c.Int());
            AddColumn("dbo.Videos", "AtistId", c => c.Int());
            CreateIndex("dbo.Photos", "ArtistId");
            CreateIndex("dbo.Photos", "VenueId");
            CreateIndex("dbo.Tracks", "ArtistId");
            CreateIndex("dbo.Videos", "AtistId");
            AddForeignKey("dbo.Photos", "ArtistId", "dbo.Artists", "ArtistId");
            AddForeignKey("dbo.Photos", "VenueId", "dbo.Venues", "VenueId");
            AddForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists", "ArtistId");
            AddForeignKey("dbo.Videos", "AtistId", "dbo.Artists", "ArtistId");
            DropColumn("dbo.Artists", "PhotoId");
            DropColumn("dbo.Artists", "VideoId");
            DropColumn("dbo.Artists", "track1Id");
            DropColumn("dbo.Artists", "track2Id");
            DropColumn("dbo.Artists", "track3Id");
            DropColumn("dbo.Venues", "PhotoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Venues", "PhotoId", c => c.Int());
            AddColumn("dbo.Artists", "track3Id", c => c.Int());
            AddColumn("dbo.Artists", "track2Id", c => c.Int());
            AddColumn("dbo.Artists", "track1Id", c => c.Int());
            AddColumn("dbo.Artists", "VideoId", c => c.Int());
            AddColumn("dbo.Artists", "PhotoId", c => c.Int());
            DropForeignKey("dbo.Videos", "AtistId", "dbo.Artists");
            DropForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Photos", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Photos", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Videos", new[] { "AtistId" });
            DropIndex("dbo.Tracks", new[] { "ArtistId" });
            DropIndex("dbo.Photos", new[] { "VenueId" });
            DropIndex("dbo.Photos", new[] { "ArtistId" });
            DropColumn("dbo.Videos", "AtistId");
            DropColumn("dbo.Tracks", "ArtistId");
            DropColumn("dbo.Photos", "VenueId");
            DropColumn("dbo.Photos", "ArtistId");
            CreateIndex("dbo.Venues", "PhotoId");
            CreateIndex("dbo.Artists", "track3Id");
            CreateIndex("dbo.Artists", "track2Id");
            CreateIndex("dbo.Artists", "track1Id");
            CreateIndex("dbo.Artists", "VideoId");
            CreateIndex("dbo.Artists", "PhotoId");
            AddForeignKey("dbo.Venues", "PhotoId", "dbo.Photos", "PhotoId");
            AddForeignKey("dbo.Artists", "VideoId", "dbo.Videos", "VideoId");
            AddForeignKey("dbo.Artists", "track3Id", "dbo.Tracks", "TraclId");
            AddForeignKey("dbo.Artists", "track2Id", "dbo.Tracks", "TraclId");
            AddForeignKey("dbo.Artists", "track1Id", "dbo.Tracks", "TraclId");
            AddForeignKey("dbo.Artists", "PhotoId", "dbo.Photos", "PhotoId");
        }
    }
}
