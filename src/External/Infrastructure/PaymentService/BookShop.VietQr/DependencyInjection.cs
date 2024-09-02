namespace BookShop.VietQr;

using BookShop.Application.Shared.QRCode;
using BookShop.VietQr.Handler;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Configure services for QR code layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void ConfigureVietQRCodeService(this IServiceCollection services)
    {
        services.AddSingleton<IQRCodeHandler, VietQRCodeHandler>();
    }
}
