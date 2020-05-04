using BattleCards.Models;
using BattleCards.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        ICollection<Card> GetAll();

        int AddCard(AddCardInputModel model, string userId);

        ICollection<Card> GetAllFromCollection(string userId);

        bool RemoveFromCollection(string userId, int cardId);

        void AddCardToCollection(string userId, int cardId);

        UserCard GetUserCard(string userId, int cardId);
    }
}
