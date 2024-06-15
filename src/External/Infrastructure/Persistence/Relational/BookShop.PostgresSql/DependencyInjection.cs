using BookShop.PostgresSql.ServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.PostgresSql;

/// <summary>
///     Configure services for data access layer.
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
    public static void ConfigurePostgreSqlDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigurePostgreSqlDbContextPool(configuration: configuration);
        services.ConfigureCore();
        services.ConfigureAspNetCoreIdentity(configuration: configuration);
    }
}
