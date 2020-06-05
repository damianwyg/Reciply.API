using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reciply.API.Data.Interfaces;
using Reciply.API.Dtos;
using Reciply.API.Models;

namespace Reciply.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReciplyRepository _repo;

        public UsersController(IReciplyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userFromRepo = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailsDto>(userFromRepo);

            return Ok(userToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersFromRepo = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserForListingDto>>(usersFromRepo);

            return Ok(usersToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAllChanges())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("follow/{recipientId}")]
        public async Task<IActionResult> FollowUser(int recipientId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var follow = await _repo.GetFollow(userId, recipientId);

            if (follow != null)
                return BadRequest("You already followed this user");

            if (await _repo.GetUser(recipientId) == null)
                return NotFound();

            follow = new Follow
            {
                FollowerId = userId,
                FolloweeId = recipientId
            };

            _repo.Add<Follow>(follow);

            if (await _repo.SaveAllChanges())
                return Ok();

            return BadRequest("Failed to follow user");
        }
    }
}