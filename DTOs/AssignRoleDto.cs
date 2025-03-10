using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class AssignRoleDto:BaseRoleDto
    {
       
        [Required]
        public string Role { get; set; }
    }
}
