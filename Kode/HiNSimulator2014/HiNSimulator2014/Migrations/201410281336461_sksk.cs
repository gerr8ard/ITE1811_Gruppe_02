namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sksk : DbMigration
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
                        LocationIDOne = c.Int(nullable: false),
                        LocationIDTwo = c.Int(nullable: false),
                        LocationOne_LocationID = c.Int(),
                        LocationTwo_LocationID = c.Int(),
                    })
                .PrimaryKey(t => t.LocationConnectionID)
                .ForeignKey("dbo.Locations", t => t.LocationOne_LocationID)
                .ForeignKey("dbo.Locations", t => t.LocationTwo_LocationID)
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PlayerName = c.String(),
                        Score = c.Int(nullable: false),
                        WritePermission = c.Boolean(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CurrentLocation_LocationID", "dbo.Locations");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LocationConnections", "LocationTwo_LocationID", "dbo.Locations");
            DropForeignKey("dbo.LocationConnections", "LocationOne_LocationID", "dbo.Locations");
            DropForeignKey("dbo.ArtificialPlayers", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "ImageID", "dbo.Images");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "CurrentLocation_LocationID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LocationConnections", new[] { "LocationTwo_LocationID" });
            DropIndex("dbo.LocationConnections", new[] { "LocationOne_LocationID" });
            DropIndex("dbo.Locations", new[] { "ImageID" });
            DropIndex("dbo.ArtificialPlayers", new[] { "LocationID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LocationConnections");
            DropTable("dbo.Commands");
            DropTable("dbo.Images");
            DropTable("dbo.Locations");
            DropTable("dbo.ArtificialPlayers");
        }
    }
}
