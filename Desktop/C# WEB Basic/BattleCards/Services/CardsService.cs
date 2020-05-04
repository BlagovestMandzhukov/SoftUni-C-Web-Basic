using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddCard(AddCardInputModel model, string userId)
        {
            var card = new Card
            {
                Name = model.Name,
                Keyword = model.Keyword,
                ImageUrl = model.Image,
                Health = model.Health,
                Attack = model.Attack,
                Description = model.Description,
            };
            var user = this.db.Users.Find(userId);
            this.db.Cards.Add(card);
            this.db.SaveChanges();
            var userCard = new UserCard
            {
                CardId = card.Id,
                UserId = user.Id,
            };
            this.db.UsersCards.Add(userCard);
            this.db.SaveChanges();
            return card.Id;
        }

        public void AddCardToCollection(string userId, int cardId)
        {
            var usersCard = this.GetUserCard(userId, cardId);
            if (usersCard == null)
            {
                var userCard = new UserCard
                {
                    CardId = cardId,
                    UserId = userId,
                };
                this.db.UsersCards.Add(userCard);
                this.db.SaveChanges();
            }
        }

        public ICollection<Card> GetAll()
        {
            var cards = this.db.Cards.ToList();
            return cards;
        }

        public ICollection<Card> GetAllFromCollection(string userId)
        {
            var cardsId = this.db.UsersCards
                .Where(x => x.UserId == userId)
                .Select(x => x.CardId)
                .ToList();

            var cards = new List<Card>();
            foreach (var id in cardsId)
            {
                var card = this.db.Cards.Where(x => x.Id == id).FirstOrDefault();
                cards.Add(card);
            }

            return cards;
        }

        public UserCard GetUserCard(string userId, int cardId)
        {
            var userCard = this.db.UsersCards.Where(x => x.UserId == userId && x.CardId == cardId).FirstOrDefault();
            if (userCard == null)
            {
                return null;
            }
            return userCard;
        }

        public bool RemoveFromCollection(string userId, int cardId)
        {
            var userCard = this.GetUserCard(userId, cardId);

            if (userCard == null)
            {
                return false;
            }

            this.db.UsersCards.Remove(userCard);
            this.db.SaveChanges();
            return true;
        }
    }
}
