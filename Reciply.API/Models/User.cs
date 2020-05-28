using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        public string  Email { get; set; }

        public byte[] PaswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
