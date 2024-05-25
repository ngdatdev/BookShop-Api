using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.DataAccess;

/// <summary>
///     Entry to configuring multiple services
///     of sql relational database.
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
    public static void AddRelationalDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigureSqlServerDbContextPool(configuration: configuration);
    }

    /// <summary>
    ///     Configure the db context pool service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureSqlServerDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration
    ) { }

    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCore(this IServiceCollection services) { }

    /// <summary>
    ///     Configure asp net core identity service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureAspNetCoreIdentity(
        this IServiceCollection services,
        IConfigurationManager configuration
    ) { }
}
