namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Extension Method for login features.
/// </summary>
public static class LoginExtensionMethod
{
    public static string ToAppCode(this LoginResponseStatusCode statusCode)
    {
        return $"{nameof(Login)}Feature: {statusCode}";
    }
}
