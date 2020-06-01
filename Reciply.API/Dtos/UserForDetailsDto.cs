using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reciply.API.Models;

namespace Reciply.API.Dtos
{
    public class UserForDetailsDto
    {
        public int UserId { get; set; }

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
    }
}
