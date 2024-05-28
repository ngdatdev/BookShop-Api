using BookShop.Application.Cache;
using BookShop.Application.Cache.RedisHandler;
using BookShop.Shared.Configuration.CacheRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application;

/// <summary>
///     Configure services for application/business layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    public static void ConfigApplication(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigCore();
        services.ConfigJwtIdentity();
        services.ConfigureStackChangeRedis(configuration: configuration);
    }

    /// <summary>
    ///     Config to core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigCore(this IServiceCollection services) { }

    /// <summary>
    ///     Config to jwt identity services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigJwtIdentity(this IServiceCollection services) { }

    /// <summary>
    ///     Configure stack change redis services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureStackChangeRedis(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        var option = configuration
            .GetRequiredSection(key: "Cache")
            .GetRequiredSection(key: "Redis")
            .Get<RedisOption>();

        services.AddStackExchangeRedisCache(setupAction: config =>
        {
            config.Configuration = option.ConnectionString;
        });
        services.AddScoped<ICacheHandler, RedisCacheHandler>();
    }
}
