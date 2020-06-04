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
    [Route("api/users/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeForAddDto recipeForAddDto)
        {
            var userFromRepo = await _repo.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            var recipeToCreate = _mapper.Map<Recipe>(recipeForAddDto);

            userFromRepo.Recipes.Add(recipeToCreate);

            if (await _repo.SaveAllChanges())
                return NoContent();

            throw new Exception($"Adding recipe failed on save");
        }

        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var recipeFromRepo = await _repo.GetRecipe(recipeId);

            _repo.Delete(recipeFromRepo);

            if (await _repo.SaveAllChanges())
                return NoContent();

            throw new Exception("Error deleting the message");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipeFromRepo = await _repo.GetRecipe(id);

            if (recipeFromRepo == null)
                return NotFound();

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

            var recipesToReturn = _mapper.Map<IEnumerable<RecipeForUserListingDto>>(recipesFromRepo);

            return Ok(recipesToReturn);
        }

        [HttpPut("{recipeId}")]
        public async Task<IActionResult> UpdateRecipe(int recipeId, RecipeForUpdateDto recipeForUpdateDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var recipeFromRepo = await _repo.GetRecipe(recipeId);

            if (userId != recipeFromRepo.UserId)
                return Unauthorized();

            _mapper.Map(recipeForUpdateDto, recipeFromRepo);

            if (await _repo.SaveAllChanges())
                return NoContent();

            throw new Exception($"Updating recipe {recipeId} failed on save");
        }
    }
}