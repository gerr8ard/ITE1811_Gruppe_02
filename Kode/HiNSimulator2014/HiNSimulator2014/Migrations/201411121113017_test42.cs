namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test42 : DbMigration
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
                "dbo.ArtificialPlayers",
                c => new
                    {
                        ArtificialPlayerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        AccessLevel = c.String(),
                        IsStationary = c.Boolean(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArtificialPlayerID)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        LocationType = c.String(),
                        AcessTypeRole = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        ImageID = c.Int(),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.Images", t => t.ImageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageText = c.String(),
                        ImageBlob = c.Binary(),
                    })
                .PrimaryKey(t => t.ImageID);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PlayerName = c.String(),
                        Score = c.Int(nullable: false),
                        WritePermission = c.Boolean(nullable: false),
                        AccessLevel = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CurrentLocation_LocationID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.CurrentLocation_LocationID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CurrentLocation_LocationID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Commands",
                c => new
                    {
                        CommandID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CommandID);
            
            CreateTable(
                "dbo.LocationConnections",
                c => new
                    {
                        LocationConnectionID = c.Int(nullable: false, identity: true),
                        IsLocked = c.Boolean(nullable: false),
                        RequiredKeyLevel = c.Int(nullable: false),
                        LocationOne_LocationID = c.Int(nullable: false),
                        LocationTwo_LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationConnectionID)
                .ForeignKey("dbo.Locations", t => t.LocationOne_LocationID, cascadeDelete: false)
                .ForeignKey("dbo.Locations", t => t.LocationTwo_LocationID, cascadeDelete: false)
                .Index(t => t.LocationOne_LocationID)
                .Index(t => t.LocationTwo_LocationID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LocationConnections", "LocationTwo_LocationID", "dbo.Locations");
            DropForeignKey("dbo.LocationConnections", "LocationOne_LocationID", "dbo.Locations");
            DropForeignKey("dbo.ChatLogs", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.ChatLogs", "FromPlayer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CurrentLocation_LocationID", "dbo.Locations");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArtificialPlayerResponses", "ArtificialPlayerID", "dbo.ArtificialPlayers");
            DropForeignKey("dbo.ArtificialPlayers", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "ImageID", "dbo.Images");
            DropIndex("dbo.ValidCommandsForThings", new[] { "CommandID" });
            DropIndex("dbo.ValidCommandsForThings", new[] { "ThingID" });
            DropIndex("dbo.ValidCommandsForArtificialPlayers", new[] { "CommandID" });
            DropIndex("dbo.ValidCommandsForArtificialPlayers", new[] { "ArtificialPlayerID" });
            DropIndex("dbo.Things", new[] { "CurrentOwner_Id" });
            DropIndex("dbo.Things", new[] { "LocationID" });
            DropIndex("dbo.Things", new[] { "ImageID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LocationConnections", new[] { "LocationTwo_LocationID" });
            DropIndex("dbo.LocationConnections", new[] { "LocationOne_LocationID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "CurrentLocation_LocationID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ChatLogs", new[] { "FromPlayer_Id" });
            DropIndex("dbo.ChatLogs", new[] { "LocationID" });
            DropIndex("dbo.Locations", new[] { "ImageID" });
            DropIndex("dbo.ArtificialPlayers", new[] { "LocationID" });
            DropIndex("dbo.ArtificialPlayerResponses", new[] { "ArtificialPlayerID" });
            DropTable("dbo.ValidCommandsForThings");
            DropTable("dbo.ValidCommandsForArtificialPlayers");
            DropTable("dbo.Things");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LocationConnections");
            DropTable("dbo.Commands");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ChatLogs");
            DropTable("dbo.Images");
            DropTable("dbo.Locations");
            DropTable("dbo.ArtificialPlayers");
            DropTable("dbo.ArtificialPlayerResponses");
        }
    }
}
