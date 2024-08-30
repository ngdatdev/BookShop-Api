namespace BookShop.Application.Features.Auth.LoginByGoogle;

/// <summary>
///     Extension Method for login features.
/// </summary>
public static class LoginByGoogleExtensionMethod
{
    public static string ToAppCode(this LoginByGoogleResponseStatusCode statusCode)
    {
        return $"{nameof(LoginByGoogle)}Feature: {statusCode}";
    }
}
