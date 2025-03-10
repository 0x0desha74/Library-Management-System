using Bookly.APIs.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bookly.APIs.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
