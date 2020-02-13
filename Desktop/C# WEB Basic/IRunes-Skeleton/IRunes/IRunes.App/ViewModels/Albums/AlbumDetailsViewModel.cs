using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.ViewModels.Albums
{
    public class AlbumDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public IEnumerable<AlbumTracksViewModel> Tracks { get; set; }
    }
}
