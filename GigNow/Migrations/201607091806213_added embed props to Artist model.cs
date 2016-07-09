namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedembedpropstoArtistmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "BandCampEmbed1", c => c.String());
            AddColumn("dbo.Artists", "BandCampEmbed2", c => c.String());
            AddColumn("dbo.Artists", "BandCampEmbed3", c => c.String());
            DropColumn("dbo.Artists", "ExtraLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ExtraLink", c => c.String());
            DropColumn("dbo.Artists", "BandCampEmbed3");
            DropColumn("dbo.Artists", "BandCampEmbed2");
            DropColumn("dbo.Artists", "BandCampEmbed1");
        }
    }
}
