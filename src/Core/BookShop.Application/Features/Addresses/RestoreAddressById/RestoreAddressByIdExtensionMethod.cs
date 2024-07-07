namespace BookShop.Application.Features.Addresses.RestoreAddressById;

/// <summary>
///     Extension Method for RestoreAddressById features.
/// </summary>
public static class RestoreAddressByIdExtensionMethod
{
    public static string ToAppCode(this RestoreAddressByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreAddressById)}Feature: {statusCode}";
    }
}
