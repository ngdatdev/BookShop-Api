using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application.Shared.ServiceConfigs;

/// <summary>
///     Fluent validation service config.
/// </summary>
internal static class ValidationConfig
{
    /// <summary>
    ///     Config to validation services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(type: typeof(DependencyInjection));
    }
}
