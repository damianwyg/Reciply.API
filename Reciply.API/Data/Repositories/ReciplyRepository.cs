using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reciply.API.Data.Interfaces;
using Reciply.API.Models;

namespace Reciply.API.Data.Repositories
{
    public class ReciplyRepository : IReciplyRepository
    {
        private readonly AppDbContext _context;

        public ReciplyRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _context
                .Comments
                .FirstOrDefaultAsync(c => c.CommentId == id);

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetComments(int recipeId)
        {
            var comments = await _context.Comments
                .Include(u => u.User)
                .Include(r => r.Recipe)
                    .Where(c => c.Recipe.RecipeId == recipeId)
                .OrderByDescending(c => c.DateAdded)
                .ToListAsync();

            return comments;
        }

        public async Task<Follow> GetFollow(int userId, int recipientId)
        {
            var follow = await _context.Followers
                .FirstOrDefaultAsync(u => u.FollowerId == userId && u.FolloweeId == recipientId);

            return follow;
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(i => i.Ingredients)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            return recipe;
        }

        public async Task<PagedList<Recipe>> GetRecipes(int loggedUserId, RecipeParams recipeParams)
        {
            var recipes = _context.Recipes
                .Include(i => i.Ingredients)
                .OrderByDescending(r => r.DateAdded)
                .AsQueryable();

            if (recipeParams.IsVegan == true)
                recipes = recipes.Where(r => r.IsVegan == true);

            if (recipeParams.IsVegetarian == true)
                recipes = recipes.Where(r => r.IsVegetarian == true);

            if (recipeParams.SearchQuery != null && recipeParams.SearchQuery != "null") // check for ingredient
                recipes = recipes.Where(r => r.Name.Contains(recipeParams.SearchQuery) || r.Ingredients.Any(i => i.Name.Contains(recipeParams.SearchQuery)));

            if (recipeParams.Followers) // people that logged user follows
            {
                var userLikers = await GetUserFollows(loggedUserId, recipeParams.Followers);
                recipes = recipes.Where(u => userLikers.Contains(u.UserId));
            }

            if (recipeParams.Followees) // people that are followed by logged user
            {
                var userLikees = await GetUserFollows(loggedUserId, recipeParams.Followers);
                recipes = recipes.Where(u => userLikees.Contains(u.UserId));
            }

            return await PagedList<Recipe>.CreateAsync(recipes, recipeParams.PageNumber, recipeParams.PageSize);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesForCurrentUser(int id)
        {
            var recipes = await _context.Recipes.Where(r => r.UserId == id).Include(r => r.Ingredients).ToListAsync();

            return recipes;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .Include(r => r.Recipes)
                .FirstOrDefaultAsync(u => u.UserId == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users
                .Include(r => r.Recipes)
                .ToListAsync();

            return users;
        }

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0; // true if something was saved to db, otherwise false
        }

        private async Task<IEnumerable<int>> GetUserFollows(int id, bool followers)
        {
            var user = await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Followees)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (followers)
            {
                return user.Followers.Where(u => u.FolloweeId == id).Select(i => i.FollowerId);
            }
            else
            {
                return user.Followees.Where(u => u.FollowerId == id).Select(i => i.FolloweeId);
            }
        }
    }
}