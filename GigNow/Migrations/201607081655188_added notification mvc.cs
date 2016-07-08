namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednotificationmvc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistNotifications",
                c => new
                    {
                        ArtistNotificationId = c.Int(nullable: false, identity: true),
                        artist_ArtistId = c.Int(),
                        slot_SlotId = c.Int(),
                    })
                .PrimaryKey(t => t.ArtistNotificationId)
                .ForeignKey("dbo.Artists", t => t.artist_ArtistId)
                .ForeignKey("dbo.Slots", t => t.slot_SlotId)
                .Index(t => t.artist_ArtistId)
                .Index(t => t.slot_SlotId);
            
            CreateTable(
                "dbo.ListenerNotifications",
                c => new
                    {
                        ListenerNotificationId = c.Int(nullable: false, identity: true),
                        gig_GigId = c.Int(),
                        listener_ListenerID = c.Int(),
                    })
                .PrimaryKey(t => t.ListenerNotificationId)
                .ForeignKey("dbo.Gigs", t => t.gig_GigId)
                .ForeignKey("dbo.Listeners", t => t.listener_ListenerID)
                .Index(t => t.gig_GigId)
                .Index(t => t.listener_ListenerID);
            
            CreateTable(
                "dbo.VenueNotifications",
                c => new
                    {
                        VenueNotificationId = c.Int(nullable: false, identity: true),
                        artist_ArtistId = c.Int(),
                        slot_SlotId = c.Int(),
                        venue_VenueId = c.Int(),
                    })
                .PrimaryKey(t => t.VenueNotificationId)
                .ForeignKey("dbo.Artists", t => t.artist_ArtistId)
                .ForeignKey("dbo.Slots", t => t.slot_SlotId)
                .ForeignKey("dbo.Venues", t => t.venue_VenueId)
                .Index(t => t.artist_ArtistId)
                .Index(t => t.slot_SlotId)
                .Index(t => t.venue_VenueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenueNotifications", "venue_VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueNotifications", "slot_SlotId", "dbo.Slots");
            DropForeignKey("dbo.VenueNotifications", "artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ListenerNotifications", "listener_ListenerID", "dbo.Listeners");
            DropForeignKey("dbo.ListenerNotifications", "gig_GigId", "dbo.Gigs");
            DropForeignKey("dbo.ArtistNotifications", "slot_SlotId", "dbo.Slots");
            DropForeignKey("dbo.ArtistNotifications", "artist_ArtistId", "dbo.Artists");
            DropIndex("dbo.VenueNotifications", new[] { "venue_VenueId" });
            DropIndex("dbo.VenueNotifications", new[] { "slot_SlotId" });
            DropIndex("dbo.VenueNotifications", new[] { "artist_ArtistId" });
            DropIndex("dbo.ListenerNotifications", new[] { "listener_ListenerID" });
            DropIndex("dbo.ListenerNotifications", new[] { "gig_GigId" });
            DropIndex("dbo.ArtistNotifications", new[] { "slot_SlotId" });
            DropIndex("dbo.ArtistNotifications", new[] { "artist_ArtistId" });
            DropTable("dbo.VenueNotifications");
            DropTable("dbo.ListenerNotifications");
            DropTable("dbo.ArtistNotifications");
        }
    }
}
