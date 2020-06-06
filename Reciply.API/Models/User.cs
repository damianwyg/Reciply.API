using System;
using System.Collections.Generic;

namespace Reciply.API.Models
{
    public class User
    {
        public int UserId { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AvatarUrl { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followees { get; set; }
    }
}