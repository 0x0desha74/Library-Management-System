namespace Bookly.APIs.Entities
{
    public class Review:BaseEntity
    {
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;


    }
}
