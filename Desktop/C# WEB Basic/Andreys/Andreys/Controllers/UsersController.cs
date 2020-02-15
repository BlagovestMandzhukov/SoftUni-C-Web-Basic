using Andreys.Services;
using Andreys.ViewModels.Home;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }
        [HttpGet("/Users/Login")]
        public HttpResponse Login()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }

            return this.Redirect("/");
        }
        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            if (model.Username.Length < 4 || model.Username.Length > 10)
            {
                return this.Error("Invalid username");
            }
            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Invalid Password");
            }
            var user = this.service.GetUserId(model.Username, model.Password);
            if (user == null)
            {
                return this.Error("Username does not exist");
            }
            this.SignIn(user);
            return this.Redirect("/");

        }

        [HttpGet("/Users/Register")]
        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/Home/Index");
            }
            if (model.Username.Length < 4 || model.Username.Length > 10)
            {
                return this.Error("Invalid username");
            }
            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Invalid Password");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Passwords do not match!");
            }

            this.service.CreateUser(model.Username, model.Password, model.Email);
            return this.Redirect("/Users/Login");

        }
    }
}
