namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class petter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtificialPlayers",
                c => new
                    {
                        ArtificialPlayerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        IsStationary = c.Boolean(nullable: false),
                        CurrentLocation_LocationID = c.Int(),
                    })
                .PrimaryKey(t => t.ArtificialPlayerID)
                .ForeignKey("dbo.Locations", t => t.CurrentLocation_LocationID)
                .Index(t => t.CurrentLocation_LocationID);
            
            CreateTable(
                "dbo.Commands",
                c => new
                    {
                        CommandsID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CommandsID);
            
            CreateTable(
                "dbo.LocationConnections",
                c => new
                    {
                        LocationConnectionID = c.Int(nullable: false, identity: true),
                        IsLocked = c.Boolean(nullable: false),
                        RequiredKeyLevel = c.Int(nullable: false),
                        LocationOne_LocationID = c.Int(),
                        LocationTwo_LocationID = c.Int(),
                    })
                .PrimaryKey(t => t.LocationConnectionID)
                .ForeignKey("dbo.Locations", t => t.LocationOne_LocationID)
                .ForeignKey("dbo.Locations", t => t.LocationTwo_LocationID)
                .Index(t => t.LocationOne_LocationID)
                .Index(t => t.LocationTwo_LocationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationConnections", "LocationTwo_LocationID", "dbo.Locations");
            DropForeignKey("dbo.LocationConnections", "LocationOne_LocationID", "dbo.Locations");
            DropForeignKey("dbo.ArtificialPlayers", "CurrentLocation_LocationID", "dbo.Locations");
            DropIndex("dbo.LocationConnections", new[] { "LocationTwo_LocationID" });
            DropIndex("dbo.LocationConnections", new[] { "LocationOne_LocationID" });
            DropIndex("dbo.ArtificialPlayers", new[] { "CurrentLocation_LocationID" });
            DropTable("dbo.LocationConnections");
            DropTable("dbo.Commands");
            DropTable("dbo.ArtificialPlayers");
        }
    }
}
