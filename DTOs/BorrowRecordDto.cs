using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class BorrowRecordDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
