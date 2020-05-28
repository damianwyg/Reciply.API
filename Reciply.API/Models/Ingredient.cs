namespace Reciply.API.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}