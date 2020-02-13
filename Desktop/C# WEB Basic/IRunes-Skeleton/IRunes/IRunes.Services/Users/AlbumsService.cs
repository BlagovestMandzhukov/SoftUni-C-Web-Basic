using IRunes.Data;
using IRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.Services.Users
{
    public class AlbumsService : IAlbumsService
    {
        private readonly RunesDbContext db;

        public AlbumsService(RunesDbContext db)
        {
            this.db = db;
        }

        public void CreateAlbum(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0.0m,
            };

            this.db.Albums.Add(album);
            this.db.SaveChanges();
        }

    }
}
