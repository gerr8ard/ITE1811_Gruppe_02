namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class j : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LocationConnections", "LocationOne_LocationID1", "dbo.Locations");
            DropForeignKey("dbo.LocationConnections", "LocationTwo_LocationID1", "dbo.Locations");
            DropIndex("dbo.LocationConnections", new[] { "LocationOne_LocationID1" });
            DropIndex("dbo.LocationConnections", new[] { "LocationTwo_LocationID1" });
            DropColumn("dbo.LocationConnections", "LocationOne_LocationID");
            DropColumn("dbo.LocationConnections", "LocationTwo_LocationID");
            RenameColumn(table: "dbo.LocationConnections", name: "LocationOne_LocationID1", newName: "LocationOne_LocationID");
            RenameColumn(table: "dbo.LocationConnections", name: "LocationTwo_LocationID1", newName: "LocationTwo_LocationID");
            AlterColumn("dbo.LocationConnections", "LocationOne_LocationID", c => c.Int(nullable: false));
            AlterColumn("dbo.LocationConnections", "LocationTwo_LocationID", c => c.Int(nullable: false));
            CreateIndex("dbo.LocationConnections", "LocationOne_LocationID");
            CreateIndex("dbo.LocationConnections", "LocationTwo_LocationID");
            AddForeignKey("dbo.LocationConnections", "LocationOne_LocationID", "dbo.Locations", "LocationID", cascadeDelete: false);
            AddForeignKey("dbo.LocationConnections", "LocationTwo_LocationID", "dbo.Locations", "LocationID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationConnections", "LocationTwo_LocationID", "dbo.Locations");
            DropForeignKey("dbo.LocationConnections", "LocationOne_LocationID", "dbo.Locations");
            DropIndex("dbo.LocationConnections", new[] { "LocationTwo_LocationID" });
            DropIndex("dbo.LocationConnections", new[] { "LocationOne_LocationID" });
            AlterColumn("dbo.LocationConnections", "LocationTwo_LocationID", c => c.Int());
            AlterColumn("dbo.LocationConnections", "LocationOne_LocationID", c => c.Int());
            RenameColumn(table: "dbo.LocationConnections", name: "LocationTwo_LocationID", newName: "LocationTwo_LocationID1");
            RenameColumn(table: "dbo.LocationConnections", name: "LocationOne_LocationID", newName: "LocationOne_LocationID1");
            AddColumn("dbo.LocationConnections", "LocationTwo_LocationID", c => c.Int(nullable: false));
            AddColumn("dbo.LocationConnections", "LocationOne_LocationID", c => c.Int(nullable: false));
            CreateIndex("dbo.LocationConnections", "LocationTwo_LocationID1");
            CreateIndex("dbo.LocationConnections", "LocationOne_LocationID1");
            AddForeignKey("dbo.LocationConnections", "LocationTwo_LocationID1", "dbo.Locations", "LocationID");
            AddForeignKey("dbo.LocationConnections", "LocationOne_LocationID1", "dbo.Locations", "LocationID");
        }
    }
}
