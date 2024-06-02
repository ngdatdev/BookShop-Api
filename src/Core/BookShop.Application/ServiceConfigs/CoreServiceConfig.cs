using BookShop.Application.Services.Concrete;
using BookShop.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    /// <summary>
    ///     Config to core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserDetailService, UserDetailService>();
    }
}
