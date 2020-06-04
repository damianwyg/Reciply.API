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
    [Route("api/users/recipes/{recipeId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReciplyRepository _repo;

        public CommentsController(IReciplyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("/{id}", Name = "GetComment")] // {id} doesn't work...
        public async Task<IActionResult> GetComment(int id)
        {
            var commentFromRepo = await _repo.GetComment(id);

            if (commentFromRepo == null)
                return NotFound();

            return Ok(commentFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsForRecipe(int recipeId)
        {
            var commentsFromRepo = await _repo.GetComments(recipeId);

            if (commentsFromRepo == null)
                return NotFound();

            var commentsToReturn = _mapper.Map<IEnumerable<CommentForRecipeDetailsDto>>(commentsFromRepo);

            return Ok(commentsToReturn);
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
            {
                var commentToReturn = _mapper.Map<CommentForRecipeDetailsDto>(commentToAdd);
                return CreatedAtRoute("GetComment", new { id = commentToAdd.CommentId }, commentToReturn);
            }

            throw new Exception("Something went wrong");
        }


    }
}