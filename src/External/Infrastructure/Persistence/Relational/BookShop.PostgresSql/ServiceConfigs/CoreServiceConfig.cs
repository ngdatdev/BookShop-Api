using BookShop.Data.Features.UnitOfWork;
using BookShop.PostgresSql.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.PostgresSql.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigureCore(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
