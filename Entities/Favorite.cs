namespace Bookly.APIs.Entities
{
    public class Favorite : BaseEntity
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public Book Book { get; set; }
    }
}
