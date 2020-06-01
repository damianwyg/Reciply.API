namespace Reciply.API.Dtos
{
    public class RecipeForUserListingDto
    {
        public int RecipeId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Preparation { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
    }
}