using IRunes.App.ViewModels.Home;
using IRunes.Services.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [HttpGet("/")]
        public HttpResponse IndexLoggedIn()
        {
            if (IsUserLoggedIn())
            {
                var viewModel = new LoggedInViewModel();
                viewModel.Name = usersService.GetUsername(this.User);
                return this.View(viewModel, "Home");
            }
            return this.Index();
        }
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}
