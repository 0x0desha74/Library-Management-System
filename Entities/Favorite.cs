namespace Bookly.APIs.Entities
{
    public class Favorite:BaseEntity
    {
        public string UserId { get; set; }
        public int bookId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public Book Book { get; set; }
    }
}
