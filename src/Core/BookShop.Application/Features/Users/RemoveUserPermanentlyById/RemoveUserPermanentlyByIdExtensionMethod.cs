namespace BookShop.Application.Features.Users.RemoveUserPermanentlyById;

/// <summary>
///     Extension Method for RemoveUserPermanentlyById features.
/// </summary>
public static class RemoveUserPermanentlyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveUserPermanentlyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveUserPermanentlyById)}Feature: {statusCode}";
    }
}
