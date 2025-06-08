using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Roomies.Persistence;

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

        // Register unit of work

        return services;
    }
}