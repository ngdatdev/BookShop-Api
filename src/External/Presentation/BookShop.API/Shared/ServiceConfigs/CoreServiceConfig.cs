using BookShop.API.Endpoints.HelloWorld.Middleware.Validation;
using BookShop.API.Shared.Filter.MinimalsApi.ValidationFilter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Shared.ServiceConfigs;

/// <summary>
///     Configure services for web api layer.
/// </summary>
internal static class CoreServiceConfig
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    internal static void ConfigureCore(
        this IServiceCollection services,
        IConfigurationManager configuration
    ) { 
        services.AddScoped(typeof(ValidationFilter<>));
        services.AddScoped<HelloWorldValidationFilter>();
    }
}
