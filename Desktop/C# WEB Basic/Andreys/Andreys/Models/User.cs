using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string  Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
