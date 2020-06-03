using System.Collections.Generic;
using System.Threading.Tasks;
using Reciply.API.Models;

namespace Reciply.API.Data.Interfaces
{
    public interface IReciplyRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<Recipe> GetRecipe(int id);

        Task<IEnumerable<Recipe>> GetRecipes();

        Task<User> GetUser(int id);

        Task<IEnumerable<User>> GetUsers();

        Task<bool> SaveAllChanges();
    }
}