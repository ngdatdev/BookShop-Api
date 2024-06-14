namespace BookShop.Configuration.Infrastructure.Mail.GoogleGmail;

/// summary
///     The GoogleGmailSmtpServerOption class is used to hold smtp server mail configuration settings.
/// summary
public sealed class GoogleGmailSmtpServerOption
{
    public string Sender { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }
}
