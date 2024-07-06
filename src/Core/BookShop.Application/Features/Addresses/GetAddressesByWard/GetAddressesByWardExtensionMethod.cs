namespace BookShop.Application.Features.Addresses.GetAddressesByWard;

/// <summary>
///     Extension Method for GetAddressesByWard features.
/// </summary>
public static class GetAddressesByWardExtensionMethod
{
    public static string ToAppCode(this GetAddressesByWardResponseStatusCode statusCode)
    {
        return $"{nameof(GetAddressesByWard)}Feature: {statusCode}";
    }
}
