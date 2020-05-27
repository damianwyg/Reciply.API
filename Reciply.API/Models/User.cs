using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string  Email { get; set; }

        public byte[] PaswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
