namespace BookShop.Application.Features.Addresses.GetAllTemporarilyAddresses;

/// <summary>
///     Extension Method for GetAllTemporarilyAddresses features.
/// </summary>
public static class GetAllTemporarilyAddressesExtensionMethod
{
    public static string ToAppCode(this GetAllTemporarilyAddressesResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllTemporarilyAddresses)}Feature: {statusCode}";
    }
}
