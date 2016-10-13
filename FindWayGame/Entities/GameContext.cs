using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindWayGame.Entities
{
    public class GameContext : DbContext
    {
        public GameContext() : base("GameDatabase") { }

        public DbSet<Player> Players { get; set; }
        
        // TODO: Change column name to Games?
        public DbSet<GameInfo> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Each player has one or more games played
            // A new record in db will be created only when at least one game has been played
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Games)
                .WithRequired(g => g.Player)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
