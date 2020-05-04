using BattleCards.Services;
using BattleCards.ViewModels.Cards;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }
        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var viewModel = new AllCardsViewModel
            {
                Cards = this.cardsService.GetAll().Select(x => new CardsViewModel
                {
                    CardId = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Attack = x.Attack,
                    Health = x.Health,
                    Keyword = x.Keyword,
                    Description = x.Description,
                })
            };
            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (model.Name.Length < 5 || model.Name.Length > 15 || string.IsNullOrEmpty(model.Name))
            {
                return this.View(model);
            }
            if (model.Description.Length > 200 || string.IsNullOrEmpty(model.Description))
            {
                return this.View(model);
            }
            if (string.IsNullOrEmpty(model.Image))
            {
                return this.View(model);
            }
            if (string.IsNullOrEmpty(model.Keyword))
            {
                return this.View(model);
            }
            if (model.Attack < 0)
            {
                return this.View(model);
            }
            if (model.Health < 0)
            {
                return this.View(model);
            }
            var userId = this.Request.SessionData["UserId"];
            var cardId = this.cardsService.AddCard(model, userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.Request.SessionData["UserId"];
            var viewModel = new AllCardsViewModel
            {
                Cards = this.cardsService.GetAllFromCollection(userId).Select(x => new CardsViewModel
                {
                    CardId = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Attack = x.Attack,
                    Health = x.Health,
                    Keyword = x.Keyword,
                    Description = x.Description,
                })
            };
            return this.View(viewModel);
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.Request.SessionData["UserId"];

            var isRemoved = this.cardsService.RemoveFromCollection(userId, cardId);
            return this.Redirect("/Cards/Collection");
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.Request.SessionData["UserId"];
            var usersCard = this.cardsService.GetUserCard(userId, cardId);
            if (usersCard != null)
            {
                return this.Redirect("/Cards/All");
            }

            this.cardsService.AddCardToCollection(userId, cardId);
            return this.Redirect("/Cards/All");
        }
    }
}
