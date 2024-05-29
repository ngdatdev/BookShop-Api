using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application.ServiceConfigs;

/// <summary>
///     Jwt identity service config.
/// </summary>
public static class JwtIdentityServiceConfig
{
    /// <summary>
    ///     Config to jwt identity services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigJwtIdentity(this IServiceCollection services) { }
}
