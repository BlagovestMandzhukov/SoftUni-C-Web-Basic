using System;
using System.Collections.Generic;

namespace BattleCards.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserCards = new HashSet<UserCard>();
        }
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<UserCard> UserCards { get; set; }
    }
}
