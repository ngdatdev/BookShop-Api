namespace BookShop.Application.Features.Users.RemoveUserTemporarilyById;

/// <summary>
///     Extension Method for RemoveUserTemporarilyById features.
/// </summary>
public static class RemoveUserTemporarilyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveUserTemporarilyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveUserTemporarilyById)}Feature: {statusCode}";
    }
}
