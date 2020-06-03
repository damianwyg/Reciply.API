using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reciply.API.Models;

namespace Reciply.API.Dtos
{
    public class RecipeForDetailsDto
    {
        public int RecipeId { get; set; }

        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Preparation { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }

        public int UserId { get; set; }
        public string AvatarUrl { get; set; }
        public string DisplayName { get; set; }

        public ICollection<IngredientForRecipeDetailsDto> Ingredients { get; set; }
    }
}
