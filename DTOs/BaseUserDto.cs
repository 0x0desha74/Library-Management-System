using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class BaseUserDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}