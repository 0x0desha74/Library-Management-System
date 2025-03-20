using Bookly.APIs.Error;
using Bookly.APIs.Helpers;
using Bookly.APIs.Interfaces;
using Bookly.APIs.Repositories;
using Bookly.APIs.Services;
using Microsoft.AspNetCore.Mvc;


namespace Bookly.APIs.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IFineService), typeof(FineService));
            services.AddScoped(typeof(IBorrowService), typeof(BorrowService));
            services.AddScoped(typeof(IFavoriteService), typeof(FavoriteService));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                                                        .SelectMany(P => P.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();


                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });



            return services;

        }
    }
}
