namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hoff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtificialPlayerResponses",
                c => new
                    {
                        ArtificialPlayerResponseID = c.Int(nullable: false, identity: true),
                        ArtificialPlayerID = c.Int(nullable: false),
                        ResponseText = c.String(),
                    })
                .PrimaryKey(t => t.ArtificialPlayerResponseID)
                .ForeignKey("dbo.ArtificialPlayers", t => t.ArtificialPlayerID, cascadeDelete: true)
                .Index(t => t.ArtificialPlayerID);
            
            CreateTable(
                "dbo.ChatLogs",
                c => new
                    {
                        ChatLogID = c.Int(nullable: false, identity: true),
                        LocationID = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        MessageText = c.String(),
                        FromPlayer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ChatLogID)
                .ForeignKey("dbo.AspNetUsers", t => t.FromPlayer_Id)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID)
                .Index(t => t.FromPlayer_Id);
            
            CreateTable(
                "dbo.Things",
                c => new
                    {
                        ThingID = c.Int(nullable: false, identity: true),
                        ImageID = c.Int(),
                        LocationID = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        IsStationary = c.Boolean(nullable: false),
                        KeyLevel = c.Int(),
                        PlayerWritable = c.Boolean(nullable: false),
                        WrittenText = c.String(),
                        CurrentOwner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ThingID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.AspNetUsers", t => t.CurrentOwner_Id)
                .ForeignKey("dbo.Images", t => t.ImageID)
                .Index(t => t.ImageID)
                .Index(t => t.LocationID)
                .Index(t => t.CurrentOwner_Id);
            
            CreateTable(
                "dbo.ValidCommandsForArtificialPlayers",
                c => new
                    {
                        ValidCommandsForArtificialPlayersID = c.Int(nullable: false, identity: true),
                        ArtificialPlayerID = c.Int(nullable: false),
                        CommandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ValidCommandsForArtificialPlayersID)
                .ForeignKey("dbo.ArtificialPlayers", t => t.ArtificialPlayerID, cascadeDelete: true)
                .ForeignKey("dbo.Commands", t => t.CommandID, cascadeDelete: true)
                .Index(t => t.ArtificialPlayerID)
                .Index(t => t.CommandID);
            
            CreateTable(
                "dbo.ValidCommandsForThings",
                c => new
                    {
                        ValidCommandsForThingsID = c.Int(nullable: false, identity: true),
                        ThingID = c.Int(nullable: false),
                        CommandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ValidCommandsForThingsID)
                .ForeignKey("dbo.Commands", t => t.CommandID, cascadeDelete: true)
                .ForeignKey("dbo.Things", t => t.ThingID, cascadeDelete: true)
                .Index(t => t.ThingID)
                .Index(t => t.CommandID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidCommandsForThings", "ThingID", "dbo.Things");
            DropForeignKey("dbo.ValidCommandsForThings", "CommandID", "dbo.Commands");
            DropForeignKey("dbo.ValidCommandsForArtificialPlayers", "CommandID", "dbo.Commands");
            DropForeignKey("dbo.ValidCommandsForArtificialPlayers", "ArtificialPlayerID", "dbo.ArtificialPlayers");
            DropForeignKey("dbo.Things", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Things", "CurrentOwner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Things", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ChatLogs", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ChatLogs", "FromPlayer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArtificialPlayerResponses", "ArtificialPlayerID", "dbo.ArtificialPlayers");
            DropIndex("dbo.ValidCommandsForThings", new[] { "CommandID" });
            DropIndex("dbo.ValidCommandsForThings", new[] { "ThingID" });
            DropIndex("dbo.ValidCommandsForArtificialPlayers", new[] { "CommandID" });
            DropIndex("dbo.ValidCommandsForArtificialPlayers", new[] { "ArtificialPlayerID" });
            DropIndex("dbo.Things", new[] { "CurrentOwner_Id" });
            DropIndex("dbo.Things", new[] { "LocationID" });
            DropIndex("dbo.Things", new[] { "ImageID" });
            DropIndex("dbo.ChatLogs", new[] { "FromPlayer_Id" });
            DropIndex("dbo.ChatLogs", new[] { "LocationID" });
            DropIndex("dbo.ArtificialPlayerResponses", new[] { "ArtificialPlayerID" });
            DropTable("dbo.ValidCommandsForThings");
            DropTable("dbo.ValidCommandsForArtificialPlayers");
            DropTable("dbo.Things");
            DropTable("dbo.ChatLogs");
            DropTable("dbo.ArtificialPlayerResponses");
        }
    }
}
