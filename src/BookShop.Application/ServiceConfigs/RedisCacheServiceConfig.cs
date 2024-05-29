using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Application.Cache;
using BookShop.Application.Cache.RedisHandler;
using BookShop.Shared.Configuration.CacheRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application.ServiceConfigs;

/// <summary>
///     Redis cache service config.
/// </summary>
public static class RedisCacheServiceConfig
{
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
    internal static void ConfigureStackChangeRedis(
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
