using Reciply.API.Models;

namespace Reciply.API.Dtos
{
    public class IngredientForDetailsDto
    {
        public int IngredientId { get; set; }

        public int Quantity { get; set; }
        public string Unit { get; set; }
        public int IngredientNameId { get; set; }
        public string Name { get; set; } // name from IngredientNames table
    }
}