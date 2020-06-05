using System;
using System.Collections.Generic;

namespace Reciply.API.Dtos
{
    public class RecipeForUpdateDto
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Preparation { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public DateTime DateAdded { get; set; }

        public ICollection<IngredientForRecipeAddDto> Ingredients { get; set; }
    }
}