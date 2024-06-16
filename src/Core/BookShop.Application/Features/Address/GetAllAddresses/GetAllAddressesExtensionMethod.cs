namespace BookShop.Application.Features.Address.GetAllAddresses;

/// <summary>
///     Extension Method for GetAllAddresses features.
/// </summary>
public static class GetAllAddressesExtensionMethod
{
    public static string ToAppCode(this GetAllAddressesResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllAddresses)}Feature: {statusCode}";
    }
}
