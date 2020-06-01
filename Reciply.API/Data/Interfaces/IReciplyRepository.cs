using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reciply.API.Models;

namespace Reciply.API.Data.Interfaces
{
    public interface IReciplyRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllChanges();
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}
