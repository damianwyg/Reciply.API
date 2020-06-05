namespace Reciply.API.Data
{
    public class RecipeParams
    {
        private const int _maxPageSize = 50;
        private int _pageSize = 10;
        public string Ingredient { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; } // restricting max page size
        }
    }
}