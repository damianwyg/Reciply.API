using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reciply.API.Data.Interfaces;
using Reciply.API.Dtos;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipeFromRepo = await _repo.GetRecipe(id);

            var recipeToReturn = _mapper.Map<RecipeForDetailsDto>(recipeFromRepo);

            return Ok(recipeToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipesFromRepo = await _repo.GetRecipes();

            var recipesToReturn = _mapper.Map<IEnumerable<RecipeForDetailsDto>>(recipesFromRepo);

            return Ok(recipesToReturn);
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> GetIngredients()
        {
            var ingredients = await _repo.GetIngredientNames();

            return Ok(ingredients);
        }
    }
}