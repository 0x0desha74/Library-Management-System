namespace Bookly.APIs.Entities
{
    public class BorrowRecord : BaseEntity
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Fine Fine { get; set; }
    }
}
