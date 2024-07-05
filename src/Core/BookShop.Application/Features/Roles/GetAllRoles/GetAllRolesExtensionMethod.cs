namespace BookShop.Application.Features.Roles.GetAllRoles;

/// <summary>
///     Extension Method for GetAllRoles features.
/// </summary>
public static class GetAllRolesExtensionMethod
{
    public static string ToAppCode(this GetAllRolesResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllRoles)}Feature: {statusCode}";
    }
}
