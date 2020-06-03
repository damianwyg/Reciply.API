using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Dtos
{
    public class RecipeForAddDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Preparation { get; set; }

        public string PhotoUrl { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }

        public ICollection<IngredientForRecipeAddDto> Ingredients { get; set; }
    }
}
