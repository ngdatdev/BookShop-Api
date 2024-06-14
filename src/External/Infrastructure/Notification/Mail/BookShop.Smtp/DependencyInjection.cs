using BookShop.Application.Shared.Mail;
using BookShop.Configuration.Infrastructure.Mail.GoogleGmail;
using BookShop.Smtp.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Smtp;

/// <summary>
///     Configure services for smtp layer.
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
    public static void ConfigGoogleSmtpMailNotification(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddSingleton<ISendingMailHandler, GoogleSendingMailHandler>();

        services.AddSingleton(
            configuration
                .GetRequiredSection(key: "SmtpServer")
                .GetRequiredSection(key: "GoogleGmail")
                .Get<GoogleGmailSendingOption>()
        );
    }
}
