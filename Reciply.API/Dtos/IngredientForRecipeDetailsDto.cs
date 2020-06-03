using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Dtos
{
    public class IngredientForRecipeDetailsDto
    {
        public int IngredientId { get; set; }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
