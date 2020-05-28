using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Reciply.API.Models;

namespace Reciply.API.Data
{
    public class Seed
    {
        public static void SeedUsers(AppDbContext context)
        {
            if (!context.Users.Any()) // check if the db is empty
            {
                var usersFromFile = System.IO.File.ReadAllText("Data/SeedData.json");
                var users = JsonSerializer.Deserialize<List<User>>(usersFromFile);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    GeneratePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }

        private static void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
