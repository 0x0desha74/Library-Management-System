using Bookly.APIs.Entities;

namespace Bookly.APIs.DTOs
{
    public class BorrowRecordToReturnDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public FineToReturnDto Fine { get; set; }
    }
}
