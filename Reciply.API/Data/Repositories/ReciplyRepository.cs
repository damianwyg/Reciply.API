using System.Collections.Generic;
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

        public async Task<Recipe> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.Include(i => i.Ingredients).FirstOrDefaultAsync(r => r.RecipeId == id);

            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            var recipes = await _context.Recipes.Include(i => i.Ingredients).ToListAsync();

            return recipes;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(r => r.Recipes).FirstOrDefaultAsync(u => u.UserId == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(r => r.Recipes).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0; // true if something was saved to db, otherwise false
        }
    }
}