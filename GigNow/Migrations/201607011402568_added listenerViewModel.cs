namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlistenerViewModel : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.ListenerViewModel)
                .ForeignKey("dbo.Addresses", t => t.address_AddressId)
                .ForeignKey("dbo.Cities", t => t.city_CityId)
                .ForeignKey("dbo.Listeners", t => t.listener_ListenerID)
                .ForeignKey("dbo.States", t => t.state_StateId)
                .ForeignKey("dbo.Zipcodes", t => t.zipcode_ZipcodeId)
                .Index(t => t.address_AddressId)
                .Index(t => t.city_CityId)
                .Index(t => t.listener_ListenerID)
                .Index(t => t.state_StateId)
                .Index(t => t.zipcode_ZipcodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListenerViewModelVMs", "zipcode_ZipcodeId", "dbo.Zipcodes");
            DropForeignKey("dbo.ListenerViewModelVMs", "state_StateId", "dbo.States");
            DropForeignKey("dbo.ListenerViewModelVMs", "listener_ListenerID", "dbo.Listeners");
            DropForeignKey("dbo.ListenerViewModelVMs", "city_CityId", "dbo.Cities");
            DropForeignKey("dbo.ListenerViewModelVMs", "address_AddressId", "dbo.Addresses");
            DropIndex("dbo.ListenerViewModelVMs", new[] { "zipcode_ZipcodeId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "state_StateId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "listener_ListenerID" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "city_CityId" });
            DropIndex("dbo.ListenerViewModelVMs", new[] { "address_AddressId" });
            DropTable("dbo.ListenerViewModelVMs");
        }
    }
}
