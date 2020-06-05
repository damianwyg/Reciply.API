using System.Collections.Generic;
using System.Threading.Tasks;
using Reciply.API.Models;

namespace Reciply.API.Data.Interfaces
{
    public interface IReciplyRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<Comment> GetComment(int id);

        Task<IEnumerable<Comment>> GetComments(int recipeId);

        Task<Follow> GetFollow(int userId, int recipientId);

        Task<Recipe> GetRecipe(int id);

        Task<PagedList<Recipe>> GetRecipes(int loggedUserId, RecipeParams recipeParams);

        Task<User> GetUser(int id);

        Task<IEnumerable<User>> GetUsers();

        Task<bool> SaveAllChanges();
    }
}