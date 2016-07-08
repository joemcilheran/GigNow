namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmessageandtypetonotif : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtistNotifications", "message", c => c.String());
            AddColumn("dbo.ArtistNotifications", "type", c => c.String());
            AddColumn("dbo.ListenerNotifications", "message", c => c.String());
            AddColumn("dbo.ListenerNotifications", "type", c => c.String());
            AddColumn("dbo.VenueNotifications", "message", c => c.String());
            AddColumn("dbo.VenueNotifications", "type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VenueNotifications", "type");
            DropColumn("dbo.VenueNotifications", "message");
            DropColumn("dbo.ListenerNotifications", "type");
            DropColumn("dbo.ListenerNotifications", "message");
            DropColumn("dbo.ArtistNotifications", "type");
            DropColumn("dbo.ArtistNotifications", "message");
        }
    }
}
