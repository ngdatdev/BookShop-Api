using BookShop.Application.IdentityService;
using BookShop.Application.IdentityService.Jwt;
using BookShop.Application.IdentityService.JwtHandler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

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
    internal static void ConfigJwtIdentity(this IServiceCollection services)
    {
        services.AddSingleton<JsonWebTokenHandler>();

        services
            .AddSingleton<IAccessTokenHandler, AccessTokenHandler>()
            .AddSingleton<IRefreshTokenHandler, RefreshTokenHandler>();
    }
}
