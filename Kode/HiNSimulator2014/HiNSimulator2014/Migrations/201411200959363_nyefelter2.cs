namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nyefelter2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Things", name: "CurrentArtificialPlayerOwner_ArtificialPlayerID", newName: "ArtificialPlayerID");
            RenameIndex(table: "dbo.Things", name: "IX_CurrentArtificialPlayerOwner_ArtificialPlayerID", newName: "IX_ArtificialPlayerID");
            DropColumn("dbo.Things", "CurrentArtificialPlayerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Things", "CurrentArtificialPlayerID", c => c.Int());
            RenameIndex(table: "dbo.Things", name: "IX_ArtificialPlayerID", newName: "IX_CurrentArtificialPlayerOwner_ArtificialPlayerID");
            RenameColumn(table: "dbo.Things", name: "ArtificialPlayerID", newName: "CurrentArtificialPlayerOwner_ArtificialPlayerID");
        }
    }
}
