using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Shared.ServiceConfigs;

/// <summary>
///     Filter services config.
/// </summary>
internal static class FilterServiceConfig
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigFilter(this IServiceCollection services)
    {
        services.AddScoped(typeof(Filter.ControllerBase.ValidationFilter.ValidationRequestFilter<>));
        services.AddScoped(typeof(Filter.MinimalsApi.ValidationFilter.ValidationFilter<>));
    }
}
