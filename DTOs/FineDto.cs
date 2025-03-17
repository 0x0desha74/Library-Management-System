using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class FineDto
    {
        [Required]
        public string UserId { get; set; }
      
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
