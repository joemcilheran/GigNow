namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class turnFKintovirtualobjects : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Addresses", name: "ZipCodeId", newName: "zipcode_ZipcodeId");
            RenameColumn(table: "dbo.Zipcodes", name: "CityId", newName: "city_CityId");
            RenameColumn(table: "dbo.Cities", name: "StateId", newName: "state_StateId");
            RenameColumn(table: "dbo.ArtistRelationships", name: "ArtistId", newName: "Artist_ArtistId");
            RenameColumn(table: "dbo.ArtistRelationships", name: "ListenerId", newName: "Listener_ListenerID");
            RenameColumn(table: "dbo.Artists", name: "AddressId", newName: "address_AddressId");
            RenameColumn(table: "dbo.Listeners", name: "AddressId", newName: "address_AddressId");
            RenameColumn(table: "dbo.GigRelationships", name: "GigId", newName: "Gig_GigId");
            RenameColumn(table: "dbo.GigRelationships", name: "ListenerId", newName: "Listener_ListenerID");
            RenameColumn(table: "dbo.Gigs", name: "VenueId", newName: "Venue_VenueId");
            RenameColumn(table: "dbo.Venues", name: "AddressId", newName: "address_AddressId");
            RenameColumn(table: "dbo.Photos", name: "VenueId", newName: "Venue_VenueId");
            RenameColumn(table: "dbo.Slots", name: "ArtistId", newName: "Artist_ArtistId");
            RenameColumn(table: "dbo.Slots", name: "GigId", newName: "Gig_GigId");
            RenameColumn(table: "dbo.Tracks", name: "ArtistId", newName: "Artist_ArtistId");
            RenameColumn(table: "dbo.VenueRelationships", name: "ListenerId", newName: "Listener_ListenerID");
            RenameColumn(table: "dbo.VenueRelationships", name: "VenueId", newName: "Venue_VenueId");
            RenameColumn(table: "dbo.Videos", name: "AtistId", newName: "Artist_ArtistId");
            RenameIndex(table: "dbo.Addresses", name: "IX_ZipCodeId", newName: "IX_zipcode_ZipcodeId");
            RenameIndex(table: "dbo.Zipcodes", name: "IX_CityId", newName: "IX_city_CityId");
            RenameIndex(table: "dbo.Cities", name: "IX_StateId", newName: "IX_state_StateId");
            RenameIndex(table: "dbo.ArtistRelationships", name: "IX_ArtistId", newName: "IX_Artist_ArtistId");
            RenameIndex(table: "dbo.ArtistRelationships", name: "IX_ListenerId", newName: "IX_Listener_ListenerID");
            RenameIndex(table: "dbo.Artists", name: "IX_AddressId", newName: "IX_address_AddressId");
            RenameIndex(table: "dbo.Listeners", name: "IX_AddressId", newName: "IX_address_AddressId");
            RenameIndex(table: "dbo.GigRelationships", name: "IX_GigId", newName: "IX_Gig_GigId");
            RenameIndex(table: "dbo.GigRelationships", name: "IX_ListenerId", newName: "IX_Listener_ListenerID");
            RenameIndex(table: "dbo.Gigs", name: "IX_VenueId", newName: "IX_Venue_VenueId");
            RenameIndex(table: "dbo.Venues", name: "IX_AddressId", newName: "IX_address_AddressId");
            RenameIndex(table: "dbo.Photos", name: "IX_VenueId", newName: "IX_Venue_VenueId");
            RenameIndex(table: "dbo.Slots", name: "IX_ArtistId", newName: "IX_Artist_ArtistId");
            RenameIndex(table: "dbo.Slots", name: "IX_GigId", newName: "IX_Gig_GigId");
            RenameIndex(table: "dbo.Tracks", name: "IX_ArtistId", newName: "IX_Artist_ArtistId");
            RenameIndex(table: "dbo.VenueRelationships", name: "IX_ListenerId", newName: "IX_Listener_ListenerID");
            RenameIndex(table: "dbo.VenueRelationships", name: "IX_VenueId", newName: "IX_Venue_VenueId");
            RenameIndex(table: "dbo.Videos", name: "IX_AtistId", newName: "IX_Artist_ArtistId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Videos", name: "IX_Artist_ArtistId", newName: "IX_AtistId");
            RenameIndex(table: "dbo.VenueRelationships", name: "IX_Venue_VenueId", newName: "IX_VenueId");
            RenameIndex(table: "dbo.VenueRelationships", name: "IX_Listener_ListenerID", newName: "IX_ListenerId");
            RenameIndex(table: "dbo.Tracks", name: "IX_Artist_ArtistId", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.Slots", name: "IX_Gig_GigId", newName: "IX_GigId");
            RenameIndex(table: "dbo.Slots", name: "IX_Artist_ArtistId", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.Photos", name: "IX_Venue_VenueId", newName: "IX_VenueId");
            RenameIndex(table: "dbo.Venues", name: "IX_address_AddressId", newName: "IX_AddressId");
            RenameIndex(table: "dbo.Gigs", name: "IX_Venue_VenueId", newName: "IX_VenueId");
            RenameIndex(table: "dbo.GigRelationships", name: "IX_Listener_ListenerID", newName: "IX_ListenerId");
            RenameIndex(table: "dbo.GigRelationships", name: "IX_Gig_GigId", newName: "IX_GigId");
            RenameIndex(table: "dbo.Listeners", name: "IX_address_AddressId", newName: "IX_AddressId");
            RenameIndex(table: "dbo.Artists", name: "IX_address_AddressId", newName: "IX_AddressId");
            RenameIndex(table: "dbo.ArtistRelationships", name: "IX_Listener_ListenerID", newName: "IX_ListenerId");
            RenameIndex(table: "dbo.ArtistRelationships", name: "IX_Artist_ArtistId", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.Cities", name: "IX_state_StateId", newName: "IX_StateId");
            RenameIndex(table: "dbo.Zipcodes", name: "IX_city_CityId", newName: "IX_CityId");
            RenameIndex(table: "dbo.Addresses", name: "IX_zipcode_ZipcodeId", newName: "IX_ZipCodeId");
            RenameColumn(table: "dbo.Videos", name: "Artist_ArtistId", newName: "AtistId");
            RenameColumn(table: "dbo.VenueRelationships", name: "Venue_VenueId", newName: "VenueId");
            RenameColumn(table: "dbo.VenueRelationships", name: "Listener_ListenerID", newName: "ListenerId");
            RenameColumn(table: "dbo.Tracks", name: "Artist_ArtistId", newName: "ArtistId");
            RenameColumn(table: "dbo.Slots", name: "Gig_GigId", newName: "GigId");
            RenameColumn(table: "dbo.Slots", name: "Artist_ArtistId", newName: "ArtistId");
            RenameColumn(table: "dbo.Photos", name: "Venue_VenueId", newName: "VenueId");
            RenameColumn(table: "dbo.Venues", name: "address_AddressId", newName: "AddressId");
            RenameColumn(table: "dbo.Gigs", name: "Venue_VenueId", newName: "VenueId");
            RenameColumn(table: "dbo.GigRelationships", name: "Listener_ListenerID", newName: "ListenerId");
            RenameColumn(table: "dbo.GigRelationships", name: "Gig_GigId", newName: "GigId");
            RenameColumn(table: "dbo.Listeners", name: "address_AddressId", newName: "AddressId");
            RenameColumn(table: "dbo.Artists", name: "address_AddressId", newName: "AddressId");
            RenameColumn(table: "dbo.ArtistRelationships", name: "Listener_ListenerID", newName: "ListenerId");
            RenameColumn(table: "dbo.ArtistRelationships", name: "Artist_ArtistId", newName: "ArtistId");
            RenameColumn(table: "dbo.Cities", name: "state_StateId", newName: "StateId");
            RenameColumn(table: "dbo.Zipcodes", name: "city_CityId", newName: "CityId");
            RenameColumn(table: "dbo.Addresses", name: "zipcode_ZipcodeId", newName: "ZipCodeId");
        }
    }
}
