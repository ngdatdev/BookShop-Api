namespace BookShop.Application.Features.Auth.Logout;

/// <summary>
///     Extension Method for hello world features.
/// </summary>
public static class LogoutExtensionMethod
{
    public static string ToAppCode(this LogoutResponseStatusCode statusCode)
    {
        return $"{nameof(Logout)}Feature: {statusCode}";
    }
}
