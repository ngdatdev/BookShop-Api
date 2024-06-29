namespace BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     Extension Method for changing password features.
/// </summary>
public static class ConfirmUserRegistrationEmailExtensionMethod
{
    public static string ToAppCode(this ConfirmUserRegistrationEmailResponseStatusCode statusCode)
    {
        return $"{nameof(ForgotPassword)}Feature: {statusCode}";
    }
}
