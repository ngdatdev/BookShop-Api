namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Extension Method for hello world features.
/// </summary>
public static class ExtensionMethod
{
    public static string ToAppCode(this LoginResponseStatusCode statusCode)
    {
        return $"{nameof(Login)}Feature: {statusCode}";
    }
}
