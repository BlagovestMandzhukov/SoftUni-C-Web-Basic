namespace Andreys.App.Controllers
{
    using Andreys.Data;
    using Andreys.Services;
    using Andreys.ViewModels.Home;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }

        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var products = this.productsService.GetAll();
                return this.View(products, "Home");
            }
            return this.View();
        }

    }
}
