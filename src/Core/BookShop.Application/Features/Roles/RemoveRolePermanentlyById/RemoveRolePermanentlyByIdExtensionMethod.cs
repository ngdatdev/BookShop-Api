namespace BookShop.Application.Features.Roles.RemoveRolePermanentlyById;

/// <summary>
///     Extension Method for RemoveRolePermanentlyById features.
/// </summary>
public static class RemoveRolePermanentlyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveRolePermanentlyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveRolePermanentlyById)}Feature: {statusCode}";
    }
}
