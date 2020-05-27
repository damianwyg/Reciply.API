using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reciply.API.Models;

namespace Reciply.API.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
