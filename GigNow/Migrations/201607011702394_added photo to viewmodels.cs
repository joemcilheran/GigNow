namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedphototoviewmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtistViewModelVMs", "photo_PhotoId", c => c.Int());
            AddColumn("dbo.VenueViewModelVMs", "photo_PhotoId", c => c.Int());
            CreateIndex("dbo.ArtistViewModelVMs", "photo_PhotoId");
            CreateIndex("dbo.VenueViewModelVMs", "photo_PhotoId");
            AddForeignKey("dbo.ArtistViewModelVMs", "photo_PhotoId", "dbo.Photos", "PhotoId");
            AddForeignKey("dbo.VenueViewModelVMs", "photo_PhotoId", "dbo.Photos", "PhotoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenueViewModelVMs", "photo_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.ArtistViewModelVMs", "photo_PhotoId", "dbo.Photos");
            DropIndex("dbo.VenueViewModelVMs", new[] { "photo_PhotoId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "photo_PhotoId" });
            DropColumn("dbo.VenueViewModelVMs", "photo_PhotoId");
            DropColumn("dbo.ArtistViewModelVMs", "photo_PhotoId");
        }
    }
}
