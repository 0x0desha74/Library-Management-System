namespace Bookly.APIs.Entities
{
    public class Fine:BaseEntity
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public bool IsPaid { get; set; }
        public int BorrowRecordId { get; set; }
    }
}
