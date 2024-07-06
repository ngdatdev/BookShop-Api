namespace BookShop.Application.Features.Addresses.UpdateAddressById;

/// <summary>
///     Extension Method for UpdateAddressById features.
/// </summary>
public static class UpdateAddressByIdExtensionMethod
{
    public static string ToAppCode(this UpdateAddressByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateAddressById)}Feature: {statusCode}";
    }
}
