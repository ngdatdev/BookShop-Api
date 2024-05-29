using BookShop.Application.ServiceConfigs;
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
}
