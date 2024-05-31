using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.API.ServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API;

/// <summary>
///     Entry to configuring multiple services
///     of web api services.
/// </summary>
internal static class DependencyInjection
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
    internal static void ConfigWebAPI(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigAuthentication(configuration: configuration);
        services.ConfigAuthorization();
        services.ConfigureCore(configuration: configuration);
        services.ConfigCors();
    }
}
