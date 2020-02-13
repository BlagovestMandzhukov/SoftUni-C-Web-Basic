using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IRunes.Models
{
    public class Album
    {
        public Album()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
