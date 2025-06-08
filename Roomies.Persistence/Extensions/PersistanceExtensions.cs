using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Roomies.Application.Interfaces;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Persistence;
using Roomies.Persistence.Repositories;

public static class PersistanceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        // Register DbContext with SQL Server
        services.AddDbContext<RoomiesDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                x => x.MigrationsAssembly(typeof(RoomiesDbContext).Assembly.FullName)
            )
        );

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHouseholdRepository, HouseholdRepository>();

        // Register unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}