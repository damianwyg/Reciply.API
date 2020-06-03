using System.Collections.Generic;
using System.Linq;
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
    public class RecipesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReciplyRepository _repo;

        public RecipesController(IReciplyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("users/{userId}/add")]
        public async Task<IActionResult> AddRecipe(int userId, RecipeForAddDto recipeForAddDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var recipeToCreate = _mapper.Map<Recipe>(recipeForAddDto);

            userFromRepo.Recipes.Add(recipeToCreate);

            if (await _repo.SaveAllChanges())
            {
                return StatusCode(201);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipeFromRepo = await _repo.GetRecipe(id);
            var userFromRepo = await _repo.GetUser(recipeFromRepo.UserId);

            var recipeToReturn = _mapper.Map<RecipeForDetailsDto>(recipeFromRepo);

            recipeToReturn.AvatarUrl = userFromRepo.AvatarUrl;
            recipeToReturn.DisplayName = userFromRepo.DisplayName;

            return Ok(recipeToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipesFromRepo = await _repo.GetRecipes();

            var recipesToReturn = _mapper.Map<IEnumerable<RecipeForDetailsDto>>(recipesFromRepo);

            return Ok(recipesToReturn);
        }
    }
}