namespace BookShop.Application.Features.Roles.CreateRole;

/// <summary>
///     Extension Method for CreateRole features.
/// </summary>
public static class CreateRoleExtensionMethod
{
    public static string ToAppCode(this CreateRoleResponseStatusCode statusCode)
    {
        return $"{nameof(CreateRole)}Feature: {statusCode}";
    }
}
