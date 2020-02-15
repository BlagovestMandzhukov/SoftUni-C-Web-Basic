using Andreys.Data;
using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Andreys.Services
{
    public class UsersService : IUsersService
    {
        private readonly AndreysDbContext db;

        public UsersService(AndreysDbContext db)
        {
            this.db = db;
        }
        public void CreateUser(string name, string password, string email)
        {
            var passwordHash = Sha256Hash(password);
            var user = new User
            {
                Username = name,
                Email = email,
                Password = passwordHash,
            };
            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string name, string password)
        {
            var hashedPassword = Sha256Hash(password);
            var user = this.db.Users.Where(x => x.Username == name && x.Password == hashedPassword)
                .Select(x => x.Id).FirstOrDefault();

            return user;
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
    }
}
