using System.Linq;
using System.Reflection;
using BookShop.API.Shared.Filter.AuthorizationFilter;
using BookShop.API.Shared.Filter.ValidationRequestFilter;
using Microsoft.AspNetCore.Mvc.Filters;
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
        Assembly assembly = typeof(DependencyInjection).Assembly;

        var filterTypes = assembly
            .GetTypes()
            .Where(t => typeof(IAsyncActionFilter).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var filterType in filterTypes)
        {
            services.AddScoped(filterType);
        }

        services.AddScoped(typeof(ValidationRequestFilter<>));

        services.AddScoped(typeof(AuthorizationFilter));
    }
}
