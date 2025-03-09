using Bookly.APIs.Data.Identity;
using Bookly.APIs.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bookly.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();


            services.AddAuthentication();


            return services;
        }

    }
}
