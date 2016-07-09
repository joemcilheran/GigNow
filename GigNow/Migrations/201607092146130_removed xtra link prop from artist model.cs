namespace GigNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedxtralinkpropfromartistmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Artists", "ExtraLink2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ExtraLink2", c => c.String());
        }
    }
}
