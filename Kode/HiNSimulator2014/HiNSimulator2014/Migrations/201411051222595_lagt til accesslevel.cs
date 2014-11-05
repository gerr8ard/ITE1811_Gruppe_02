namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lagttilaccesslevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtificialPlayers", "AccessLevel", c => c.String());
            AddColumn("dbo.AspNetUsers", "AccessLevel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccessLevel");
            DropColumn("dbo.ArtificialPlayers", "AccessLevel");
        }
    }
}
