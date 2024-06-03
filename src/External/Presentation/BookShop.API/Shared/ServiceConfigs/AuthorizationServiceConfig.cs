using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Shared.ServiceConfigs;

/// <summary>
///     Authorization service config.
/// </summary>
public static class AuthorizationServiceConfig
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}
