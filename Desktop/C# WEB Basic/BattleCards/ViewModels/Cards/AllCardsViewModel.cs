using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.ViewModels.Cards
{
    public class AllCardsViewModel
    {
        public IEnumerable<CardsViewModel> Cards { get; set; }
    }
}
