namespace Bookly.APIs.Specifications
{
    public class BooksSpecParams: PaginationSpecParams
    {
       
        public string? Genre { get; set; }
        public int? AuthorId { get; set; }
    }
}
