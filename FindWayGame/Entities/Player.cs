using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindWayGame.Entities
{
    public class Player 
    {
        public Player()
        {
            Games = new List<GameInfo>();
        }

        public int PlayerId { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }

        public Rank Rank { get; set; }

        public virtual ICollection<GameInfo> Games { get; set; }
    }
}
