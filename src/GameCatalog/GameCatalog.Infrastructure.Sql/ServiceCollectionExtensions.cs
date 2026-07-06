using GameCatalog.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameCatalog.Infrastructure.Sql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameCatalogInfrastructure(this IServiceCollection services, string connectionName)
    {
        services.AddDbContext<GameCatalogDbContext>(options =>
            options.UseSqlServer());

        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
