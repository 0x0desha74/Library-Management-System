namespace Bookly.APIs.Specifications
{
    public class PaginationSpecParams
    {
        private const int maxPageSize = 10;
        public int PageIndex { get; set; } = 1;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? maxPageSize : value; }
        }

    }
}
