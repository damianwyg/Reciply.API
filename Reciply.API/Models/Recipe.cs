using System.Collections.Generic;

namespace Reciply.API.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }

        public string Name { get; set; }
        public string Preparation { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}