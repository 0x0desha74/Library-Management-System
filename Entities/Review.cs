namespace Bookly.APIs.Entities
{
    public class Review:BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }

    }
}
