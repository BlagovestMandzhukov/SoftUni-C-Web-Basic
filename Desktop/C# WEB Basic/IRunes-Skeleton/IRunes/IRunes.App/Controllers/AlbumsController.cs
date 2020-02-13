using IRunes.App.ViewModels.Albums;
using IRunes.Data;
using IRunes.Services.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly RunesDbContext db;
        private readonly IAlbumsService albumsService;

        public AlbumsController(RunesDbContext db, IAlbumsService albumsService)
        {
            this.db = db;
            this.albumsService = albumsService;
        }
        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Home/Index");
            }
            var allAlbums = this.db.Albums.Select(x => new AlbumNameViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            var viewModel = new AllAlbumsViewModel { Albums = allAlbums };
            return this.View(viewModel, "All");
        }
        public HttpResponse Create()
        {
            if (this.IsUserLoggedIn())
            {
                return this.View();
            }
            return this.Redirect("/Home/Index");
        }
        [HttpPost]
        public HttpResponse Create(AlbumsInputViewModel model)
        {
            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Name must be between 4 and 20 symbols");
            }
            if (model.Cover.Length <= 0)
            {
                return this.Error("Invalid cover");
            }
            this.albumsService.CreateAlbum(model.Name, model.Cover);
            return this.Redirect("/Albums/All");
        }
        public HttpResponse BackToAll()
        {
            return this.Redirect("/Albums/All");
        }
        public HttpResponse Details(string id)
        {
            var albums = this.db.Albums.Select(x => new AlbumDetailsViewModel
            {
                Id = id,
                Name = x.Name,
                Price = x.Price,
                Tracks = this.db.Tracks.Select(x => new AlbumTracksViewModel
                {
                    Id = x.Id,
                    Name = x.Name,

                }).ToList()

            }).ToList();
            var viewModel = albums;
            return this.View(viewModel, "Details");
        }
    }
}
