using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class AuthorDto
    {
       
        public int Id{ get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Bio { get; set; }
        [Required]
        public DateOnly BirthDate { get; set; }
       
        public DateOnly? DeathDate { get; set; }
    }
}
