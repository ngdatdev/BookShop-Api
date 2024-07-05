namespace BookShop.Application.Features.Roles.GetAllRolesTemporarilyRemoved;

/// <summary>
///     Extension Method for GetAllRolesTemporarilyRemoved features.
/// </summary>
public static class GetAllRolesTemporarilyRemovedExtensionMethod
{
    public static string ToAppCode(this GetAllRolesTemporarilyRemovedResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllRolesTemporarilyRemoved)}Feature: {statusCode}";
    }
}
