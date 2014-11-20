namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nyefelter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtificialPlayers", "ImageID", c => c.Int());
            AddColumn("dbo.Images", "MimeType", c => c.String());
            AddColumn("dbo.Things", "CurrentArtificialPlayerID", c => c.Int());
            AddColumn("dbo.Things", "CurrentArtificialPlayerOwner_ArtificialPlayerID", c => c.Int());
            CreateIndex("dbo.ArtificialPlayers", "ImageID");
            CreateIndex("dbo.Things", "CurrentArtificialPlayerOwner_ArtificialPlayerID");
            AddForeignKey("dbo.ArtificialPlayers", "ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.Things", "CurrentArtificialPlayerOwner_ArtificialPlayerID", "dbo.ArtificialPlayers", "ArtificialPlayerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Things", "CurrentArtificialPlayerOwner_ArtificialPlayerID", "dbo.ArtificialPlayers");
            DropForeignKey("dbo.ArtificialPlayers", "ImageID", "dbo.Images");
            DropIndex("dbo.Things", new[] { "CurrentArtificialPlayerOwner_ArtificialPlayerID" });
            DropIndex("dbo.ArtificialPlayers", new[] { "ImageID" });
            DropColumn("dbo.Things", "CurrentArtificialPlayerOwner_ArtificialPlayerID");
            DropColumn("dbo.Things", "CurrentArtificialPlayerID");
            DropColumn("dbo.Images", "MimeType");
            DropColumn("dbo.ArtificialPlayers", "ImageID");
        }
    }
}
