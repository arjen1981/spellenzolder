using Microsoft.Extensions.DependencyInjection;

namespace GameCatalog.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameCatalogApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<IGameRepository>();
        });

        return services;
    }
}
