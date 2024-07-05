namespace BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     Extension Method for RemoveRoleTemporarilyById features.
/// </summary>
public static class RemoveRoleTemporarilyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveRoleTemporarilyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveRoleTemporarilyById)}Feature: {statusCode}";
    }
}
