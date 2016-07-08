namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endofday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtistNotifications", "read", c => c.Boolean(nullable: false));
            AddColumn("dbo.ListenerNotifications", "read", c => c.Boolean(nullable: false));
            AddColumn("dbo.VenueNotifications", "read", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VenueNotifications", "read");
            DropColumn("dbo.ListenerNotifications", "read");
            DropColumn("dbo.ArtistNotifications", "read");
        }
    }
}
