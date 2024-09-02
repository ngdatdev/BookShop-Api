namespace BookShop.PayOSGateway;

using BookShop.Configuration.Infrastructure.PayOS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.payOS;

/// <summary>
///     Configure services for PayOS layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Configuration manager.
    /// </param>
    public static void ConfigPayOSService(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        var option = configuration.GetRequiredSection(key: "PayOS").Get<PayOSOption>();

        services.AddSingleton(() =>
        {
            return new PayOS(clientId: option.ClientId, option.ApiKey, option.ChecksumKey);
        });
    }
}
