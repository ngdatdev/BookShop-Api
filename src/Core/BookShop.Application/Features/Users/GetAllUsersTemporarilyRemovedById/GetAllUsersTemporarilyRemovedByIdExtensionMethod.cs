namespace BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;

/// <summary>
///     Extension Method for GetAllUsersTemporarilyRemovedById features.
/// </summary>
public static class GetAllUsersTemporarilyRemovedByIdExtensionMethod
{
    public static string ToAppCode(
        this GetAllUsersTemporarilyRemovedByIdResponseStatusCode statusCode
    )
    {
        return $"{nameof(GetAllUsersTemporarilyRemovedById)}Feature: {statusCode}";
    }
}
