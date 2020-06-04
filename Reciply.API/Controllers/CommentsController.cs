using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reciply.API.Data.Interfaces;
using Reciply.API.Models;

namespace Reciply.API.Controllers
{
    [Route("api/recipes/{recipeId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IReciplyRepository _repo;

        public CommentsController(IReciplyRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int recipeId, Comment comment)
        {
            var recipeFromRepo = await _repo.GetRecipe(recipeId);
            var userFromRepo = await _repo.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var commentToAdd = new Comment
            {
                Message = comment.Message,
                DateAdded = DateTime.Now,
                Recipe = recipeFromRepo,
                User = userFromRepo
            };

            recipeFromRepo.Comments.Add(commentToAdd);

            if (await _repo.SaveAllChanges())
                return NoContent();

            throw new Exception("Something went wrong");
        }
    }
}