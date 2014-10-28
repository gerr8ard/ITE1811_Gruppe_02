namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "LocationName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "LocationName");
        }
    }
}
