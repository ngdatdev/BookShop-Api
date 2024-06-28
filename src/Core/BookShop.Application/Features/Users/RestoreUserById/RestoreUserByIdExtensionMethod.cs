namespace BookShop.Application.Features.Users.RestoreUserById;

/// <summary>
///     Extension Method for RestoreUserById features.
/// </summary>
public static class RestoreUserByIdExtensionMethod
{
    public static string ToAppCode(this RestoreUserByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreUserById)}Feature: {statusCode}";
    }
}
