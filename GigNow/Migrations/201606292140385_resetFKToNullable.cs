namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetFKToNullable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(),
                        Apt = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.ArtistRelationships",
                c => new
                    {
                        ArtistRelationshipId = c.Int(nullable: false, identity: true),
                        ListenerId = c.Int(),
                        ArtistId = c.Int(),
                    })
                .PrimaryKey(t => t.ArtistRelationshipId)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Listeners", t => t.ListenerId)
                .Index(t => t.ListenerId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressId = c.Int(),
                        ContactName = c.String(),
                        Genre1 = c.String(),
                        Genre2 = c.String(),
                        Genre3 = c.String(),
                        Type = c.String(),
                        NumberOfMembers = c.Int(),
                        PhotoId = c.Int(),
                        VideoId = c.Int(),
                        track1Id = c.Int(),
                        track2Id = c.Int(),
                        track3Id = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Photos", t => t.PhotoId)
                .ForeignKey("dbo.Tracks", t => t.track1Id)
                .ForeignKey("dbo.Tracks", t => t.track2Id)
                .ForeignKey("dbo.Tracks", t => t.track3Id)
                .ForeignKey("dbo.Videos", t => t.VideoId)
                .Index(t => t.AddressId)
                .Index(t => t.PhotoId)
                .Index(t => t.VideoId)
                .Index(t => t.track1Id)
                .Index(t => t.track2Id)
                .Index(t => t.track3Id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TraclId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.TraclId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.VideoId);
            
            CreateTable(
                "dbo.Listeners",
                c => new
                    {
                        ListenerID = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        Genre1 = c.String(),
                        Genre2 = c.String(),
                        Genre3 = c.String(),
                    })
                .PrimaryKey(t => t.ListenerID)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ZipCodeId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Zipcodes", t => t.ZipCodeId)
                .Index(t => t.ZipCodeId);
            
            CreateTable(
                "dbo.Zipcodes",
                c => new
                    {
                        ZipcodeId = c.Int(nullable: false, identity: true),
                        ZipCode = c.Int(),
                        AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.ZipcodeId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.GigRelationships",
                c => new
                    {
                        GigRelationshipId = c.Int(nullable: false, identity: true),
                        ListenerId = c.Int(),
                        GigId = c.Int(),
                    })
                .PrimaryKey(t => t.GigRelationshipId)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .ForeignKey("dbo.Listeners", t => t.ListenerId)
                .Index(t => t.ListenerId)
                .Index(t => t.GigId);
            
            CreateTable(
                "dbo.Gigs",
                c => new
                    {
                        GigId = c.Int(nullable: false, identity: true),
                        Cover = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        DefaultGenre = c.String(),
                        DefaulCompensation = c.Int(),
                        Name = c.String(),
                        UseVenueDefaults = c.Boolean(nullable: false),
                        VenueId = c.Int(),
                    })
                .PrimaryKey(t => t.GigId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        VenueId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressId = c.Int(),
                        Capacity = c.Int(),
                        StageSize = c.Int(),
                        ContactName = c.String(),
                        DefaultArtistType = c.String(),
                        DefaultCompensation = c.Int(),
                        DefaultGenre = c.String(),
                        DefaultPerks = c.String(),
                        FBLink = c.String(),
                        SiteLink = c.String(),
                        TwitterLink = c.String(),
                        ExtraLink = c.String(),
                        ExtraLink2 = c.String(),
                        SoundSystem = c.Boolean(nullable: false),
                        LoadInInstructions = c.String(),
                        rating = c.Int(),
                        PhotoId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VenueId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Photos", t => t.PhotoId)
                .Index(t => t.AddressId)
                .Index(t => t.PhotoId)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.Slots",
                c => new
                    {
                        SlotId = c.Int(nullable: false, identity: true),
                        Compensation = c.Int(),
                        Genre = c.String(),
                        IsFilled = c.Boolean(nullable: false),
                        ArtistType = c.String(),
                        Perks = c.String(),
                        Order = c.String(),
                        Length = c.Int(),
                        UseGigDefaults = c.Boolean(nullable: false),
                        GigId = c.Int(),
                    })
                .PrimaryKey(t => t.SlotId)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .Index(t => t.GigId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityId = c.Int(),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.VenueRelationships",
                c => new
                    {
                        VenueRelationshipId = c.Int(nullable: false, identity: true),
                        ListenerId = c.Int(),
                        VenueId = c.Int(),
                    })
                .PrimaryKey(t => t.VenueRelationshipId)
                .ForeignKey("dbo.Listeners", t => t.ListenerId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.ListenerId)
                .Index(t => t.VenueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenueRelationships", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueRelationships", "ListenerId", "dbo.Listeners");
            DropForeignKey("dbo.States", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Slots", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.GigRelationships", "ListenerId", "dbo.Listeners");
            DropForeignKey("dbo.GigRelationships", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Gigs", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Venues", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Venues", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Venues", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Cities", "ZipCodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.Zipcodes", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistRelationships", "ListenerId", "dbo.Listeners");
            DropForeignKey("dbo.Listeners", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Listeners", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistRelationships", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Artists", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.Artists", "track3Id", "dbo.Tracks");
            DropForeignKey("dbo.Artists", "track2Id", "dbo.Tracks");
            DropForeignKey("dbo.Artists", "track1Id", "dbo.Tracks");
            DropForeignKey("dbo.Artists", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Artists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Artists", "AddressId", "dbo.Addresses");
            DropIndex("dbo.VenueRelationships", new[] { "VenueId" });
            DropIndex("dbo.VenueRelationships", new[] { "ListenerId" });
            DropIndex("dbo.States", new[] { "CityId" });
            DropIndex("dbo.Slots", new[] { "GigId" });
            DropIndex("dbo.Venues", new[] { "UserId" });
            DropIndex("dbo.Venues", new[] { "PhotoId" });
            DropIndex("dbo.Venues", new[] { "AddressId" });
            DropIndex("dbo.Gigs", new[] { "VenueId" });
            DropIndex("dbo.GigRelationships", new[] { "GigId" });
            DropIndex("dbo.GigRelationships", new[] { "ListenerId" });
            DropIndex("dbo.Zipcodes", new[] { "AddressId" });
            DropIndex("dbo.Cities", new[] { "ZipCodeId" });
            DropIndex("dbo.Listeners", new[] { "UserId" });
            DropIndex("dbo.Listeners", new[] { "AddressId" });
            DropIndex("dbo.Artists", new[] { "UserId" });
            DropIndex("dbo.Artists", new[] { "track3Id" });
            DropIndex("dbo.Artists", new[] { "track2Id" });
            DropIndex("dbo.Artists", new[] { "track1Id" });
            DropIndex("dbo.Artists", new[] { "VideoId" });
            DropIndex("dbo.Artists", new[] { "PhotoId" });
            DropIndex("dbo.Artists", new[] { "AddressId" });
            DropIndex("dbo.ArtistRelationships", new[] { "ArtistId" });
            DropIndex("dbo.ArtistRelationships", new[] { "ListenerId" });
            DropTable("dbo.VenueRelationships");
            DropTable("dbo.States");
            DropTable("dbo.Slots");
            DropTable("dbo.Messages");
            DropTable("dbo.Venues");
            DropTable("dbo.Gigs");
            DropTable("dbo.GigRelationships");
            DropTable("dbo.Zipcodes");
            DropTable("dbo.Cities");
            DropTable("dbo.Listeners");
            DropTable("dbo.Videos");
            DropTable("dbo.Tracks");
            DropTable("dbo.Photos");
            DropTable("dbo.Artists");
            DropTable("dbo.ArtistRelationships");
            DropTable("dbo.Addresses");
        }
    }
}
