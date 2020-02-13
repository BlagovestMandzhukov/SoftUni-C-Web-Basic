using IRunes.App.ViewModels.Home;
using IRunes.Services.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }
            return this.Redirect("/Home/Index");
        }
        [HttpPost]
        public HttpResponse Login(LoginInputViewModel model)
        {
            if (model.Username.Length < 4 || model.Username.Length > 10)
            {
                return this.Error("Username must be between 4 and 10 symbols!");
            }
            if (model.Password.Length < 4 || model.Password.Length > 20)
            {
                return this.Error("Password must be between 4 and 20 symbols!");
            }
            var user = usersService.GetUserId(model.Username, model.Password);
            if (user == null)
            {
                return this.Error("Invalid username or password!");
            }
            this.SignIn(user);
            return this.Redirect("/");
        }
        public HttpResponse Register()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }
            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputViewModel model)
        {
            if (model.Username.Length < 4 || model.Username.Length > 10)
            {
                return this.Error("Username must be between 4 and 10 symbols!");
            }
            if (model.Password.Length < 4 || model.Password.Length > 20)
            {
                return this.Error("Password must be between 4 and 20 symbols!");
            }
            if (model.Email.Length < 0)
            {
                return this.Error("Invalid email");
            }
            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("The passwords you entered do not match!");
            }
            if (usersService.UsernameExists(model.Username))
            {
                return this.Error("Username already exists!");
            }
            if (usersService.EmailExists(model.Email))
            {
                return this.Error("Email already exists!");
            }
            usersService.AddUser(model.Username, model.Password, model.Email);
            
            return this.Redirect("Login");
        }
        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
