namespace BookShop.Application.Features.Users.GetAllUsers;

/// <summary>
///     Extension Method for GetAllUsers features.
/// </summary>
public static class GetAllUsersExtensionMethod
{
    public static string ToAppCode(this GetAllUsersResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllUsers)}Feature: {statusCode}";
    }
}
