namespace BookShop.Application.Features.Auth.RegisterAsUser;

/// <summary>
///     Extension Method for RegisterAsUser features.
/// </summary>
public static class RegisterAsUserExtensionMethod
{
    public static string ToAppCode(this RegisterAsUserResponseStatusCode statusCode)
    {
        return $"{nameof(RegisterAsUser)}Feature: {statusCode}";
    }
}
