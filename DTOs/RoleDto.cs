using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class RoleDto:BaseRoleDto
    {
        [Required]
        public string Role { get; set; }

    }
}
