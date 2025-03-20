using System.ComponentModel.DataAnnotations;

namespace Bookly.APIs.DTOs
{
    public class RegisterDto : BaseUserDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
      ErrorMessage = "Password must have 1 Uppercase , 1 Lowercase , 1 Number , 1 non-alphanumeric and at least 6 characters")]
        public string Password { get; set; }

    }
}
