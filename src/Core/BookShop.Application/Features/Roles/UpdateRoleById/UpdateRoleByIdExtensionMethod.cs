namespace BookShop.Application.Features.Roles.UpdateRoleById;

/// <summary>
///     Extension Method for UpdateRoleById features.
/// </summary>
public static class UpdateRoleByIdExtensionMethod
{
    public static string ToAppCode(this UpdateRoleByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateRoleById)}Feature: {statusCode}";
    }
}
