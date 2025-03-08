using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class BookForAuthorDto
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateOnly PublishedDate { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int TotalCount { get; set; }
        [Required]
        public int AvailableCount { get; set; }
    }
}
