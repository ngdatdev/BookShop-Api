namespace BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     Extension Method for RemoveOrderPermanentlyById features.
/// </summary>
public static class RemoveOrderPermanentlyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveOrderPermanentlyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveOrderPermanentlyById)}Feature: {statusCode}";
    }
}
