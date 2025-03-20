namespace Bookly.APIs.DTOs
{
    public class BookToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public DateOnly PublishedDate { get; set; }
        public string Genre { get; set; }
        public string PictureUrl { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
    }
}
