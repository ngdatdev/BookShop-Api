using BookShop.DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.DataAccess.ServiceConfigs;

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
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
    }
}
