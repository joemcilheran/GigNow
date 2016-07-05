namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddefaultstogig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "DefaultArtistType", c => c.String());
            AddColumn("dbo.Gigs", "DefaultCompensation", c => c.Int());
            AddColumn("dbo.Gigs", "DefaultPerks", c => c.String());
            AddColumn("dbo.Gigs", "LoadInInsrtuctions", c => c.String());
            DropColumn("dbo.Gigs", "DefaulCompensation");
            DropColumn("dbo.Gigs", "UseVenueDefaults");
            DropColumn("dbo.Slots", "UseGigDefaults");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slots", "UseGigDefaults", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gigs", "UseVenueDefaults", c => c.Boolean(nullable: false));
            AddColumn("dbo.Gigs", "DefaulCompensation", c => c.Int());
            DropColumn("dbo.Gigs", "LoadInInsrtuctions");
            DropColumn("dbo.Gigs", "DefaultPerks");
            DropColumn("dbo.Gigs", "DefaultCompensation");
            DropColumn("dbo.Gigs", "DefaultArtistType");
        }
    }
}
