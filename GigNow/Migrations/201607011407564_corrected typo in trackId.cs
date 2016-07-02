namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctedtypointrackId : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Tracks", "TraclId", "TrackId");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Tracks", "TrackId", "TraclId");
        }
    }
}
