namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redo : DbMigration
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
                        ZipCodeId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Zipcodes", t => t.ZipCodeId)
                .Index(t => t.ZipCodeId);
            
            CreateTable(
                "dbo.Zipcodes",
                c => new
                    {
                        ZipcodeId = c.Int(nullable: false, identity: true),
                        ZipCode = c.Int(),
                        CityId = c.Int(),
                    })
                .PrimaryKey(t => t.ZipcodeId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
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
                        FBLink = c.String(),
                        SiteLink = c.String(),
                        TwitterLink = c.String(),
                        ExtraLink = c.String(),
                        ExtraLink2 = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.ArtistViewModelVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        address_AddressId = c.Int(),
                        artist_ArtistId = c.Int(),
                        city_CityId = c.Int(),
                        state_StateId = c.Int(),
                        zipcode_ZipcodeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.address_AddressId)
                .ForeignKey("dbo.Artists", t => t.artist_ArtistId)
                .ForeignKey("dbo.Cities", t => t.city_CityId)
                .ForeignKey("dbo.States", t => t.state_StateId)
                .ForeignKey("dbo.Zipcodes", t => t.zipcode_ZipcodeId)
                .Index(t => t.address_AddressId)
                .Index(t => t.artist_ArtistId)
                .Index(t => t.city_CityId)
                .Index(t => t.state_StateId)
                .Index(t => t.zipcode_ZipcodeId);
            
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
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VenueId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AddressId)
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
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                        ArtistId = c.Int(),
                        VenueId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.ArtistId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                "dbo.Tracks",
                c => new
                    {
                        TraclId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                        ArtistId = c.Int(),
                    })
                .PrimaryKey(t => t.TraclId)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .Index(t => t.ArtistId);
            
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
            
            CreateTable(
                "dbo.VenueViewModelVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        address_AddressId = c.Int(),
                        city_CityId = c.Int(),
                        state_StateId = c.Int(),
                        venue_VenueId = c.Int(),
                        zipcode_ZipcodeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.address_AddressId)
                .ForeignKey("dbo.Cities", t => t.city_CityId)
                .ForeignKey("dbo.States", t => t.state_StateId)
                .ForeignKey("dbo.Venues", t => t.venue_VenueId)
                .ForeignKey("dbo.Zipcodes", t => t.zipcode_ZipcodeId)
                .Index(t => t.address_AddressId)
                .Index(t => t.city_CityId)
                .Index(t => t.state_StateId)
                .Index(t => t.venue_VenueId)
                .Index(t => t.zipcode_ZipcodeId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                        AtistId = c.Int(),
                    })
                .PrimaryKey(t => t.VideoId)
                .ForeignKey("dbo.Artists", t => t.AtistId)
                .Index(t => t.AtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "AtistId", "dbo.Artists");
            DropForeignKey("dbo.VenueViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.VenueViewModelVMs", "venue_VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueViewModelVMs", "state_StateId", "dbo.States");
            DropForeignKey("dbo.VenueViewModelVMs", "city_CityId", "dbo.Cities");
            DropForeignKey("dbo.VenueViewModelVMs", "address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.VenueRelationships", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueRelationships", "ListenerId", "dbo.Listeners");
            DropForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Slots", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Photos", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Photos", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.GigRelationships", "ListenerId", "dbo.Listeners");
            DropForeignKey("dbo.GigRelationships", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Gigs", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Venues", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Venues", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.ArtistViewModelVMs", "state_StateId", "dbo.States");
            DropForeignKey("dbo.ArtistViewModelVMs", "city_CityId", "dbo.Cities");
            DropForeignKey("dbo.ArtistViewModelVMs", "artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ArtistViewModelVMs", "address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistRelationships", "ListenerId", "dbo.Listeners");
            DropForeignKey("dbo.Listeners", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Listeners", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistRelationships", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Artists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Artists", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "ZipCodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.Zipcodes", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.Videos", new[] { "AtistId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "zipcode_ZipcodeId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "venue_VenueId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "state_StateId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "city_CityId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "address_AddressId" });
            DropIndex("dbo.VenueRelationships", new[] { "VenueId" });
            DropIndex("dbo.VenueRelationships", new[] { "ListenerId" });
            DropIndex("dbo.Tracks", new[] { "ArtistId" });
            DropIndex("dbo.Slots", new[] { "GigId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Photos", new[] { "VenueId" });
            DropIndex("dbo.Photos", new[] { "ArtistId" });
            DropIndex("dbo.Venues", new[] { "UserId" });
            DropIndex("dbo.Venues", new[] { "AddressId" });
            DropIndex("dbo.Gigs", new[] { "VenueId" });
            DropIndex("dbo.GigRelationships", new[] { "GigId" });
            DropIndex("dbo.GigRelationships", new[] { "ListenerId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "zipcode_ZipcodeId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "state_StateId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "city_CityId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "artist_ArtistId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "address_AddressId" });
            DropIndex("dbo.Listeners", new[] { "UserId" });
            DropIndex("dbo.Listeners", new[] { "AddressId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Artists", new[] { "UserId" });
            DropIndex("dbo.Artists", new[] { "AddressId" });
            DropIndex("dbo.ArtistRelationships", new[] { "ArtistId" });
            DropIndex("dbo.ArtistRelationships", new[] { "ListenerId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropIndex("dbo.Zipcodes", new[] { "CityId" });
            DropIndex("dbo.Addresses", new[] { "ZipCodeId" });
            DropTable("dbo.Videos");
            DropTable("dbo.VenueViewModelVMs");
            DropTable("dbo.VenueRelationships");
            DropTable("dbo.Tracks");
            DropTable("dbo.Slots");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Photos");
            DropTable("dbo.Messages");
            DropTable("dbo.Venues");
            DropTable("dbo.Gigs");
            DropTable("dbo.GigRelationships");
            DropTable("dbo.ArtistViewModelVMs");
            DropTable("dbo.Listeners");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Artists");
            DropTable("dbo.ArtistRelationships");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Zipcodes");
            DropTable("dbo.Addresses");
        }
    }
}
