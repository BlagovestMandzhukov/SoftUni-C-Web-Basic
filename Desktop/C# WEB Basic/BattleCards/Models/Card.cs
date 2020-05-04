using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Models
{
    public class Card
    {
        public Card()
        {
            this.UserCards = new HashSet<UserCard>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string  ImageUrl { get; set; }

        public string Keyword { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public string Description { get; set; }

        public ICollection<UserCard> UserCards { get; set; }
    }
}
