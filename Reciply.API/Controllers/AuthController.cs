using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reciply.API.Data.Interfaces;
using Reciply.API.Models;

namespace Reciply.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            username = username.ToLower();

            if (await _repo.UserExists(username))
                return BadRequest("This email is already used");

            var userToSave = new User
            {
                Username = username,
                Email = email,
            };

            var userSaved = _repo.Register(userToSave, password);

            return StatusCode(201);
        }
    }
}