using BookShop.API.Policy.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.ServiceConfigs;

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
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                name: "VerifyUser",
                policy => policy.Requirements.Add(new ValidationUserRequirement())
            );
        });
        services.AddSingleton<IAuthorizationHandler, ValidationUserHandler>();
    }
}
