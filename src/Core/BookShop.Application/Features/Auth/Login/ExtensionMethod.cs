namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Extension Method for hello world features.
/// </summary>
public static class ExtensionMethod
{
    public static string ToAppCode(this LoginResponseStatusCode statusCode)
    {
        var messageStatusCode = statusCode.ToString().Replace("_", " ");
        return $"{nameof(Login)}Feature: {messageStatusCode}";
    }
}
