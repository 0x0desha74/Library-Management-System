namespace Bookly.APIs.Specifications
{
    public class BooksSpecParams: PaginationSpecParams
    {
       
        public string? Genre { get; set; } 
        public int? AuthorId { get; set; }
        public string? sort { get; set; }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }


        //public int? Top { get; set; }
    }
}
