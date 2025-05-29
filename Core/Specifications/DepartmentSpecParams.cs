namespace Core.Specifications
{
    public class DepartmentSpecParams
    {
        public int? Id { get; set; }

        private const int MaxPageSize = 50;
        public int PageIndex { get; set; }
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? Sort { get; set; }

        private string _search = string.Empty;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}