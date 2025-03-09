
using Bookly.APIs.Data;
using Bookly.APIs.Data.Identity;
using Bookly.APIs.Entities;
using Bookly.APIs.Extensions;
using Bookly.APIs.Helpers;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Middlewares;
using Bookly.APIs.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookly.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddIdentityServices();
            builder.Services.AddApplicationServices();



            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String Not Found");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                var cs = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection String Not Found"); ;
                options.UseSqlServer(cs);
            });



            var app = builder.Build();

            var scop = app.Services.CreateScope();
            var services = scop.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                await ApplicationDbContextSeed.DataSeedAsync(context);

                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.UserSeedAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred During Migration");
            }

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseAuthorization();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.MapControllers();

            app.Run();
        }
    }
}
