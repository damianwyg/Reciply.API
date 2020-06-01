namespace Reciply.API.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public int Quantity { get; set; }
        public string Unit { get; set; }

        public int IngredientNameId { get; set; }
        public IngredientName IngredientName { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}