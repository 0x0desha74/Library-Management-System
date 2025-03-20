using Microsoft.AspNetCore.Identity;

namespace Bookly.APIs.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;
    }
}
