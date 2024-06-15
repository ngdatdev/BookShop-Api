namespace BookShop.Application.Features.Auth.ChangingPassword;

/// <summary>
///     Extension Method for changing password features.
/// </summary>
public static class ChangingPasswordExtensionMethod
{
    public static string ToAppCode(this ChangingPasswordResponseStatusCode statusCode)
    {
        return $"{nameof(ForgotPassword)}Feature: {statusCode}";
    }
}
