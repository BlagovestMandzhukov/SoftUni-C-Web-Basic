using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.ViewModels.Albums
{
    public class AllAlbumsViewModel
    {
        public IEnumerable<AlbumNameViewModel> Albums { get; set; }
    }
}
