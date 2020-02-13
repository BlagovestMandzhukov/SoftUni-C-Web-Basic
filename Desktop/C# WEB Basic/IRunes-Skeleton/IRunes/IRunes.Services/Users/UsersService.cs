using IRunes.Data;
using IRunes.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly RunesDbContext db;

        public UsersService(RunesDbContext db)
        {
            this.db = db;
        }
        public string GetUserId(string username, string password)
        {
            var hashedPassword = Sha256Hash(password);
            var userId = db.Users.Where(x => x.Username == username && x.Password == hashedPassword).Select(x=>x.Id).FirstOrDefault();

            return userId;
        }

        static string Sha256Hash(string password)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void AddUser(string username, string password, string email)
        {
            var hashPassword = Sha256Hash(password);
            var user = new User
            {
                Username = username,
                Password = hashPassword,
                Email = email,
            };
            this.db.Users.Add(user);
            db.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return  this.db.Users.Any(x => x.Username == username);
        }

        public bool EmailExists(string email)
        {
            return this.db.Users.Any(x => x.Email == email);
        }

        public string GetUsername(string Id)
        {
            var username = this.db.Users
                .Where(x => x.Id == Id)
                .Select(n => n.Username)
                .FirstOrDefault();
            return username;
        }
    }
}
