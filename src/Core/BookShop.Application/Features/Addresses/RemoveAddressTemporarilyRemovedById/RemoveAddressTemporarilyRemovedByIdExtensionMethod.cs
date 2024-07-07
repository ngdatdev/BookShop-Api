namespace BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     Extension Method for RemoveAddressTemporarilyRemovedById features.
/// </summary>
public static class RemoveAddressTemporarilyRemovedByIdExtensionMethod
{
    public static string ToAppCode(
        this RemoveAddressTemporarilyRemovedByIdResponseStatusCode statusCode
    )
    {
        return $"{nameof(RemoveAddressTemporarilyRemovedById)}Feature: {statusCode}";
    }
}
