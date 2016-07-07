namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedcustomVMsfromdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistViewModelVMs", "address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistViewModelVMs", "artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ArtistViewModelVMs", "city_CityId", "dbo.Cities");
            DropForeignKey("dbo.ArtistViewModelVMs", "photo_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.ArtistViewModelVMs", "state_StateId", "dbo.States");
            DropForeignKey("dbo.ArtistViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.ListenerViewModelVMs", "address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ListenerViewModelVMs", "city_CityId", "dbo.Cities");
            DropForeignKey("dbo.ListenerViewModelVMs", "listener_ListenerID", "dbo.Listeners");
            DropForeignKey("dbo.ListenerViewModelVMs", "state_StateId", "dbo.States");
            DropForeignKey("dbo.ListenerViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.VenueViewModelVMs", "address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.VenueViewModelVMs", "city_CityId", "dbo.Cities");
            DropForeignKey("dbo.VenueViewModelVMs", "photo_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.VenueViewModelVMs", "state_StateId", "dbo.States");
            DropForeignKey("dbo.VenueViewModelVMs", "venue_VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes");
            DropIndex("dbo.ArtistViewModelVMs", new[] { "address_AddressId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "artist_ArtistId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "city_CityId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "photo_PhotoId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "state_StateId" });
            DropIndex("dbo.ArtistViewModelVMs", new[] { "zipcode_ZipcodeId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "address_AddressId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "city_CityId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "listener_ListenerID" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "state_StateId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "zipcode_ZipcodeId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "address_AddressId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "city_CityId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "photo_PhotoId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "state_StateId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "venue_VenueId" });
            DropIndex("dbo.VenueViewModelVMs", new[] { "zipcode_ZipcodeId" });
            DropTable("dbo.ArtistViewModelVMs");
            DropTable("dbo.ListenerViewModelVMs");
            DropTable("dbo.VenueViewModelVMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VenueViewModelVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        address_AddressId = c.Int(),
                        city_CityId = c.Int(),
                        photo_PhotoId = c.Int(),
                        state_StateId = c.Int(),
                        venue_VenueId = c.Int(),
                        zipcode_ZipcodeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ListenerViewModelVMs",
                c => new
                    {
                        ListenerViewModel = c.Int(nullable: false, identity: true),
                        address_AddressId = c.Int(),
                        city_CityId = c.Int(),
                        listener_ListenerID = c.Int(),
                        state_StateId = c.Int(),
                        zipcode_ZipcodeId = c.Int(),
                    })
                .PrimaryKey(t => t.ListenerViewModel);
            
            CreateTable(
                "dbo.ArtistViewModelVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        address_AddressId = c.Int(),
                        artist_ArtistId = c.Int(),
                        city_CityId = c.Int(),
                        photo_PhotoId = c.Int(),
                        state_StateId = c.Int(),
                        zipcode_ZipcodeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.VenueViewModelVMs", "zipcode_ZipcodeId");
            CreateIndex("dbo.VenueViewModelVMs", "venue_VenueId");
            CreateIndex("dbo.VenueViewModelVMs", "state_StateId");
            CreateIndex("dbo.VenueViewModelVMs", "photo_PhotoId");
            CreateIndex("dbo.VenueViewModelVMs", "city_CityId");
            CreateIndex("dbo.VenueViewModelVMs", "address_AddressId");
            CreateIndex("dbo.ListenerViewModelVMs", "zipcode_ZipcodeId");
            CreateIndex("dbo.ListenerViewModelVMs", "state_StateId");
            CreateIndex("dbo.ListenerViewModelVMs", "listener_ListenerID");
            CreateIndex("dbo.ListenerViewModelVMs", "city_CityId");
            CreateIndex("dbo.ListenerViewModelVMs", "address_AddressId");
            CreateIndex("dbo.ArtistViewModelVMs", "zipcode_ZipcodeId");
            CreateIndex("dbo.ArtistViewModelVMs", "state_StateId");
            CreateIndex("dbo.ArtistViewModelVMs", "photo_PhotoId");
            CreateIndex("dbo.ArtistViewModelVMs", "city_CityId");
            CreateIndex("dbo.ArtistViewModelVMs", "artist_ArtistId");
            CreateIndex("dbo.ArtistViewModelVMs", "address_AddressId");
            AddForeignKey("dbo.VenueViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes", "ZipcodeId");
            AddForeignKey("dbo.VenueViewModelVMs", "venue_VenueId", "dbo.Venues", "VenueId");
            AddForeignKey("dbo.VenueViewModelVMs", "state_StateId", "dbo.States", "StateId");
            AddForeignKey("dbo.VenueViewModelVMs", "photo_PhotoId", "dbo.Photos", "PhotoId");
            AddForeignKey("dbo.VenueViewModelVMs", "city_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.VenueViewModelVMs", "address_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.ListenerViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes", "ZipcodeId");
            AddForeignKey("dbo.ListenerViewModelVMs", "state_StateId", "dbo.States", "StateId");
            AddForeignKey("dbo.ListenerViewModelVMs", "listener_ListenerID", "dbo.Listeners", "ListenerID");
            AddForeignKey("dbo.ListenerViewModelVMs", "city_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.ListenerViewModelVMs", "address_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.ArtistViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes", "ZipcodeId");
            AddForeignKey("dbo.ArtistViewModelVMs", "state_StateId", "dbo.States", "StateId");
            AddForeignKey("dbo.ArtistViewModelVMs", "photo_PhotoId", "dbo.Photos", "PhotoId");
            AddForeignKey("dbo.ArtistViewModelVMs", "city_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.ArtistViewModelVMs", "artist_ArtistId", "dbo.Artists", "ArtistId");
            AddForeignKey("dbo.ArtistViewModelVMs", "address_AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
