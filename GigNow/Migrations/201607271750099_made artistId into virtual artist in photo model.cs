namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeartistIdintovirtualartistinphotomodel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Photos", name: "ArtistId", newName: "Artist_ArtistId");
            RenameIndex(table: "dbo.Photos", name: "IX_ArtistId", newName: "IX_Artist_ArtistId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Photos", name: "IX_Artist_ArtistId", newName: "IX_ArtistId");
            RenameColumn(table: "dbo.Photos", name: "Artist_ArtistId", newName: "ArtistId");
        }
    }
}
