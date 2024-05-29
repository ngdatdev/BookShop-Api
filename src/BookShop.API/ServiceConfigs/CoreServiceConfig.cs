using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.ServiceConfigs;

/// <summary>
///     Configure services for web api layer.
/// </summary>
internal static class CoreServiceConfig
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
    internal static void ConfigureWebAPI(
        this IServiceCollection services,
        IConfigurationManager configuration
    ) { }
}
