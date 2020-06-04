using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Reciply.API.Models;

namespace Reciply.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}