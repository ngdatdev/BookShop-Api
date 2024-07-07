namespace BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     Extension Method for RemoveAddressPermanentlyRemovedById features.
/// </summary>
public static class RemoveAddressPermanentlyRemovedByIdExtensionMethod
{
    public static string ToAppCode(
        this RemoveAddressPermanentlyRemovedByIdResponseStatusCode statusCode
    )
    {
        return $"{nameof(RemoveAddressPermanentlyRemovedById)}Feature: {statusCode}";
    }
}
