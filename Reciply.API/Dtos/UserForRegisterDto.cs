using System;
using System.ComponentModel.DataAnnotations;

namespace Reciply.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must contain 6 - 10 characters")]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string AvatarUrl { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string AboutMe { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}