namespace Bookly.APIs.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateOnly PublishedDate { get; set; }
        public string Genre { get; set; }
        public string? PictureUrl { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
    }
}
