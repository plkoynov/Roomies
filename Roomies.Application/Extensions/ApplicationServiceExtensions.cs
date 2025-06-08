using Microsoft.Extensions.DependencyInjection;
using Roomies.Application.Interfaces.Services;
using Roomies.Application.Services;

namespace Roomies.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
