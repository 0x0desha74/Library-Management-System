using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class ReviewDto
    {
        [Required]
        [Range(0,5)]
        public int Rating { get; set; }
        [Required]
        [MaxLength(2500)]
        public string Comment { get; set; }
        
    }
}
