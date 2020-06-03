using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Dtos
{
    public class IngredientForRecipeAddDto
    {
        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
