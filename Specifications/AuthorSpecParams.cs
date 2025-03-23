namespace Bookly.APIs.Specifications
{
    public class AuthorSpecParams : PaginationSpecParams
    {
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
    }
}
