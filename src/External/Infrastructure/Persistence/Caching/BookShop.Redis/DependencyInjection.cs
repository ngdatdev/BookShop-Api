using BookShop.Application.Caching.RedisHandler;
using BookShop.Application.Shared.Caching;
using BookShop.Configuration.Infrastructure.CacheRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Redis;

/// <summary>
///     Configure services for this layer.
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
    /// </param>
    public static void AddRedisCachingDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigureCore();

        services.ConfigureStackChangeRedis(configuration: configuration);
    }

    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCore(this IServiceCollection services)
    {
        services.AddScoped<ICacheHandler, RedisCacheHandler>();
    }

    /// <summary>
    ///     Configure stack change redis services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
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
    }
}
