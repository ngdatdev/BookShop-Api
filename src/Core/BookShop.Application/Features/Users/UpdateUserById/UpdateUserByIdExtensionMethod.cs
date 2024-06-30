namespace BookShop.Application.Features.Users.UpdateUserById;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateUserByIdExtensionMethod
{
    public static string ToAppCode(this UpdateUserByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateUserById)}Feature: {statusCode}";
    }
}
