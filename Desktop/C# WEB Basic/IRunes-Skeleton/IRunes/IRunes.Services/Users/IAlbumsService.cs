using IRunes.Models;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services.Users
{
    public interface IAlbumsService
    {
        void CreateAlbum(string name, string cover);
    }
}
