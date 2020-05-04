﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Models
{
    public class UserCard
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int CardId { get; set; }

        public Card Card { get; set; }
    }
}
