namespace FindWayGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttemptsInsteadOfDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameInfoes", "Attempts", c => c.Int(nullable: false));
            DropColumn("dbo.GameInfoes", "Duration");
            DropColumn("dbo.GameInfoes", "IsWon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameInfoes", "IsWon", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameInfoes", "Duration", c => c.Double(nullable: false));
            DropColumn("dbo.GameInfoes", "Attempts");
        }
    }
}
