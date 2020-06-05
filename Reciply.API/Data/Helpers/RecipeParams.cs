namespace Reciply.API.Data
{
    public class RecipeParams
    {
        private const int _maxPageSize = 50;
        private int _pageSize = 18;
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public int PageNumber { get; set; } = 1;
        public string SearchQuery { get; set; }
        public int UserId { get; set; }
        public bool Followees { get; set; } = false;
        public bool Followers { get; set; } = false;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; } // restricting max page size
        }

    }
}