namespace BookShop.Application.Features.Roles.RestoreRoleById;

/// <summary>
///     Extension Method for RestoreRoleById features.
/// </summary>
public static class RestoreRoleByIdExtensionMethod
{
    public static string ToAppCode(this RestoreRoleByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreRoleById)}Feature: {statusCode}";
    }
}
