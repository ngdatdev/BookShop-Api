using BookShop.Application.Features.HelloWorld;
using BookShop.Application.Shared.Features;
using BookShop.MediatrCustom.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.MediatrCustom;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring register class's implement IFeatHandler services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void ConfigMediatorHandlerService(this IServiceCollection services)
    {
        services.AddScoped<IMediator, MediatorHandler>();
        services.AddLogging();
        services.AddTransient<
            IFeatureHandler<HelloWorldRequest, HelloWorldResponse>,
            HelloWorldHandler
        >();
    }
}
