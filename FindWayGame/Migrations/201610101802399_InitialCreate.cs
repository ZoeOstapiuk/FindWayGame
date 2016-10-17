namespace FindWayGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameInfoes",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Duration = c.Double(nullable: false),
                        IsWon = c.Boolean(nullable: false),
                        Player_PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId, cascadeDelete: true)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameInfoes", "Player_PlayerId", "dbo.Players");
            DropIndex("dbo.GameInfoes", new[] { "Player_PlayerId" });
            DropTable("dbo.Players");
            DropTable("dbo.GameInfoes");
        }
    }
}
